using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using WorkiomProjectManagement.Permissions;
using WorkiomProjectManagement.ProjectManagement;
using WorkiomProjectManagement.ReportEngine.Dtos;

namespace WorkiomProjectManagement.ReportEngine;

[RemoteService(IsEnabled = false)]
[Authorize(WorkiomProjectManagementPermissions.Projects.ManageReports)]
public class ProjectReportAppService : WorkiomProjectManagementAppService, IProjectReportAppService
{
    protected IProjectRepository ProjectRepository => LazyServiceProvider.LazyGetRequiredService<IProjectRepository>();
    protected IProjectReportGeneratorResolver ProjectReportGeneratorResolver => LazyServiceProvider.LazyGetRequiredService<IProjectReportGeneratorResolver>();

    public async Task<ProjectReportResultDto> GenerateProjectReportAsync(Guid projectId, string projectReportSystemName, ProjectReportRequestDto input)
    {
        await ProjectRepository.EnsureExistsAsync(projectId);

        var generator = ProjectReportGeneratorResolver.Resolve(projectReportSystemName);

        input ??= new ProjectReportRequestDto();
        var projectReportRequest = ObjectMapper.Map<ProjectReportRequestDto, ProjectReportRequest>(input);

        await generator.ValidateAsync(projectReportRequest);

        var result = await generator.GenerateAsync(projectId, projectReportRequest);

        return ObjectMapper.Map<ProjectReportResult, ProjectReportResultDto>(result);
    }

    public virtual async Task<List<ProjectReportInfoDto>> GetAvailableProjectReportTypesAsync()
    {
        var availableReports = ProjectReportGeneratorResolver.GetAvailableReportsInfo().ToList();

        return ObjectMapper.Map<List<ProjectReportInfo>, List<ProjectReportInfoDto>>(availableReports);
    }
}
