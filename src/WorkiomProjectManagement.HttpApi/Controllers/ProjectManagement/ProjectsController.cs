using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using WorkiomProjectManagement.ProjectManagement;
using WorkiomProjectManagement.ProjectManagement.Dtos;
using WorkiomProjectManagement.ReportEngine;
using WorkiomProjectManagement.ReportEngine.Dtos;

namespace WorkiomProjectManagement.Controllers.ProjectManagement;

[Area("app")]
[Route("api/app/projects")]
public class ProjectsController : WorkiomProjectManagementController, IProjectAppService, IProjectReportAppService
{
    protected IProjectAppService ProjectAppService => LazyServiceProvider.LazyGetRequiredService<IProjectAppService>();
    protected IProjectReportAppService ProjectReportAppService => LazyServiceProvider.LazyGetRequiredService<IProjectReportAppService>();

    [HttpGet("{id}")]
    public virtual Task<ProjectDto> GetAsync(Guid id)
    {
        return ProjectAppService.GetAsync(id);
    }

    [HttpGet()]
    public virtual Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectsInput input)
    {
        return ProjectAppService.GetListAsync(input);
    }

    [HttpPost()]
    public virtual Task<ProjectDto> CreateAsync(CreateProjectDto input)
    {
        return ProjectAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public virtual Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto input)
    {
        return ProjectAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return ProjectAppService.DeleteAsync(id);
    }

    [HttpGet("{projectId}/members/{memberUserId}")]
    public virtual Task<ProjectMemberDto> GetMemberAsync(Guid projectId, Guid memberUserId)
    {
        return ProjectAppService.GetMemberAsync(projectId, memberUserId);
    }

    [HttpGet("{projectId}/members")]
    public virtual Task<PagedResultDto<ProjectMemberDto>> GetListMembersAsync(Guid projectId, GetProjectMembersInput input)
    {
        return ProjectAppService.GetListMembersAsync(projectId, input);
    }

    [HttpPost("{projectId}/members")]
    public virtual Task AddProjectMemberAsync(Guid projectId, AddProjectMemberDto input)
    {
        return ProjectAppService.AddProjectMemberAsync(projectId, input);
    }

    [HttpDelete("{projectId}/members")]
    public virtual Task RemoveProjectMemberDtoAsync(Guid projectId, RemoveProjectMemberDto input)
    {
        return ProjectAppService.RemoveProjectMemberDtoAsync(projectId, input);
    }

    [HttpPost("{projectId}/reports/{projectReportSystemName}")]
    public virtual Task<ProjectReportResultDto> GenerateProjectReportAsync(Guid projectId, string projectReportSystemName, ProjectReportRequestDto input)
    {
        return ProjectReportAppService.GenerateProjectReportAsync(projectId, projectReportSystemName, input);
    }

    [HttpGet("reports/types")]
    public virtual Task<List<ProjectReportInfoDto>> GetAvailableProjectReportTypesAsync()
    {
        return ProjectReportAppService.GetAvailableProjectReportTypesAsync();
    }
}