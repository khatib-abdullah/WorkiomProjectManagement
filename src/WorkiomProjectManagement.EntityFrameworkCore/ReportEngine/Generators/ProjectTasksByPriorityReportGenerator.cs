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

public class ProjectTasksByPriorityReportGenerator : ProjectReportGeneratorBase
{
    public override string ProjectReportSystemName => "project_tasks_by_priority";
    public override IReadOnlyList<ProjectReportParameterInfo> SupportedParameters
        => [_dateRangeStartParameter, _dateRangeEndParameter, _assignedUserIdParameter, _assignedOnlyParameter];

    private readonly ProjectReportParameterInfo _dateRangeStartParameter = new()
    {
        Key = "dateRangeStart",
        Type = nameof(DateOnly),
        Required = false
    };

    private readonly ProjectReportParameterInfo _dateRangeEndParameter = new()
    {
        Key = "dateRangeEnd",
        Type = nameof(DateOnly),
        Required = false
    };

    private readonly ProjectReportParameterInfo _assignedUserIdParameter = new()
    {
        Key = "assignedUserId",
        Type = nameof(Guid),
        Required = false
    };

    private readonly ProjectReportParameterInfo _assignedOnlyParameter = new()
    {
        Key = "assignedOnly",
        Type = nameof(Boolean),
        Required = false
    };

    private readonly IProjectTaskRepository _projectTaskRepository;

    public ProjectTasksByPriorityReportGenerator(
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

        var dateFrom = input.GetProperty<DateOnly?>(_dateRangeStartParameter.Key);
        var dateTo = input.GetProperty<DateOnly?>(_dateRangeEndParameter.Key);
        var assignedUserId = input.GetProperty<Guid?>(_assignedUserIdParameter.Key);
        var assignedOnly = input.GetProperty<bool?>(_assignedOnlyParameter.Key);

        DateTime? dateTimeFrom = dateFrom.HasValue ? new(dateFrom.Value, TimeOnly.MinValue) : null;
        DateTime? dateTimeTo = dateTo.HasValue ? new(dateTo.Value, TimeOnly.MaxValue) : null;

        query = query
            .Where(x => x.ProjectId == projectId)
            .WhereIf(assignedOnly.GetValueOrDefault(), x => x.AssignedUserId != null)
            .WhereIf(assignedUserId.HasValue, x => x.AssignedUserId == assignedUserId)
            .WhereIf(dateTimeFrom.HasValue, x => x.CreationTime >= dateTimeFrom)
            .WhereIf(dateTimeTo.HasValue, x => x.CreationTime <= dateTimeTo);

        var grouped = await query
            .GroupBy(t => t.Priority)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToListAsync(cancellationToken);

        return new ProjectReportResult
        {
            ProjectReportSystemName = ProjectReportSystemName,
            Labels = [.. grouped.Select(g => L[$"Enum:TaskPriority:{g.Status}"].Value)],
            Series =
            [
                new()
                {
                    Name = L["Report:Series:TaskCount"].Value,
                    Values = [.. grouped.Select(g => (decimal)g.Count)]
                }
            ]
        };
    }
}
