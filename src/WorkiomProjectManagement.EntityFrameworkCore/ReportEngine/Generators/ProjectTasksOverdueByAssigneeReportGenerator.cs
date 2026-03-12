using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WorkiomProjectManagement.Localization;
using WorkiomProjectManagement.ProjectManagement;

namespace WorkiomProjectManagement.ReportEngine.Generators;

public class ProjectTasksOverdueByAssigneeReportGenerator : ProjectReportGeneratorBase
{
    public override string ProjectReportSystemName => "project_tasks_overdue_by_assignee";
    public override IReadOnlyList<ProjectReportParameterInfo> SupportedParameters
        => [_statusParameter];

    private readonly ProjectReportParameterInfo _statusParameter = new()
    {
        Key = "status",
        Type = nameof(ProjectManagement.TaskStatus),
        Required = false
    };

    private readonly IProjectTaskRepository _projectTaskRepository;

    public ProjectTasksOverdueByAssigneeReportGenerator(
        IProjectTaskRepository projectTaskRepository,
        IStringLocalizer<WorkiomProjectManagementResource> localizer)
        : base(localizer)
    {
        _projectTaskRepository = projectTaskRepository;
    }

    public override async Task<ProjectReportResult> GenerateAsync(
        Guid projectId,
        ProjectReportRequest input,
        CancellationToken cancellationToken = default)
    {
        var query = await _projectTaskRepository.GetQueryableAsync();

        var status = input.GetProperty<ProjectManagement.TaskStatus?>(_statusParameter.Key);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        query = query
            .Where(x => x.ProjectId == projectId)
            .Where(x => x.DueDate < today)
            .Where(x => x.Status != ProjectManagement.TaskStatus.Done && x.Status != ProjectManagement.TaskStatus.Cancelled)
            .WhereIf(status.HasValue, x => x.Status == status);

        var grouped = await query
            .GroupBy(t => t.AssignedUserId)
            .Select(g => new { AssignedUserId = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        return new ProjectReportResult
        {
            ProjectReportSystemName = ProjectReportSystemName,
            Labels = [.. grouped.Select(g => g.AssignedUserId.HasValue
                ? g.AssignedUserId.Value.ToString()
                : L["Report:Label:Unassigned"].Value)],
            Series =
            [
                new()
                {
                    Name = L["Report:Series:OverdueTasks"].Value,
                    Values = [.. grouped.Select(g => (decimal)g.Count)]
                }
            ]
        };
    }
}
