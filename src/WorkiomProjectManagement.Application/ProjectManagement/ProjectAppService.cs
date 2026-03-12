using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using WorkiomProjectManagement.Permissions;
using WorkiomProjectManagement.ProjectManagement.Dtos;

namespace WorkiomProjectManagement.ProjectManagement;

[RemoteService(IsEnabled = false)]
[Authorize(WorkiomProjectManagementPermissions.Projects.Default)]
public class ProjectAppService : WorkiomProjectManagementAppService, IProjectAppService
{
    protected virtual ProjectManager ProjectManager => LazyServiceProvider.LazyGetRequiredService<ProjectManager>();
    protected virtual IProjectRepository ProjectRepository => LazyServiceProvider.LazyGetRequiredService<IProjectRepository>();
    protected virtual IProjectMemberRepository ProjectMemberRepository => LazyServiceProvider.LazyGetRequiredService<IProjectMemberRepository>();
    protected virtual IProjectTaskRepository ProjectTaskRepository => LazyServiceProvider.LazyGetRequiredService<IProjectTaskRepository>();

    public virtual async Task<ProjectDto> GetAsync(Guid id)
    {
        var project = await ProjectRepository.GetAsync(id);
        return ObjectMapper.Map<Project, ProjectDto>(project);
    }

    public virtual async Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectsInput input)
    {
        var totalCount = await ProjectRepository.GetCountAsync();
        var projects = await ProjectRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting ?? ProjectConsts.GetDefaultSorting(false),
            includeDetails: true
        );

        var projectIds = projects.Select(p => p.Id).ToList();
        var taskCounts = await ProjectTaskRepository.GetCountsPerProjectAsync(projectIds);

        var items = projects.Select(p =>
        {
            var dto = ObjectMapper.Map<Project, ProjectDto>(p);
            dto.TasksCount = taskCounts.GetValueOrDefault(p.Id, 0L);
            return dto;
        });

        return new PagedResultDto<ProjectDto>(
            totalCount,
            [.. items]
        );
    }

    [Authorize(WorkiomProjectManagementPermissions.Projects.Create)]
    public virtual async Task<ProjectDto> CreateAsync(CreateProjectDto input)
    {
        var project = await ProjectManager.CreateProjectAsync(
            input.Name,
            input.Description);

        project = await ProjectManager.AddMemberAsync(
            project,
            CurrentUser.Id!.Value);

        await ProjectRepository.InsertAsync(project);
        return ObjectMapper.Map<Project, ProjectDto>(project);
    }

    [Authorize(WorkiomProjectManagementPermissions.Projects.Edit)]
    public virtual async Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto input)
    {
        var project = await ProjectRepository.GetAsync(id);

        project = await ProjectManager.UpdateProjectAsync(
            project,
            input.Name,
            input.Description);

        project.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        await ProjectRepository.UpdateAsync(project);
        return ObjectMapper.Map<Project, ProjectDto>(project);
    }

    [Authorize(WorkiomProjectManagementPermissions.Projects.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        var project = await ProjectRepository.GetAsync(id);

        await ProjectRepository.DeleteAsync(project);
    }

    public virtual async Task<ProjectMemberDto> GetMemberAsync(Guid projectId, Guid memberUserId)
    {
        var projectMember = await ProjectMemberRepository.GetAsync(x => x.ProjectId == projectId && x.UserId == memberUserId);
        return ObjectMapper.Map<ProjectMember, ProjectMemberDto>(projectMember);
    }

    public virtual async Task<PagedResultDto<ProjectMemberDto>> GetListMembersAsync(Guid projectId, GetProjectMembersInput input)
    {
        var totalCount = await ProjectMemberRepository.CountByProjectAsync(projectId);
        var projectMembers = await ProjectMemberRepository.GetListByProjectAsync(
            projectId,
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting ?? ProjectMemberConsts.GetDefaultSorting(false),
            includeDetails: true
        );

        return new PagedResultDto<ProjectMemberDto>(
            totalCount,
            [.. projectMembers.Select(ObjectMapper.Map<ProjectMember, ProjectMemberDto>)]
        );
    }

    [Authorize(WorkiomProjectManagementPermissions.Projects.ManageMembers)]
    public virtual async Task AddProjectMemberAsync(Guid projectId, AddProjectMemberDto input)
    {
        var project = await ProjectRepository.GetAsync(projectId);

        project = await ProjectManager.AddMemberAsync(
            project,
            input.MemberUserId);

        await ProjectRepository.UpdateAsync(project);
    }

    [Authorize(WorkiomProjectManagementPermissions.Projects.ManageMembers)]
    public virtual async Task RemoveProjectMemberDtoAsync(Guid projectId, RemoveProjectMemberDto input)
    {
        var project = await ProjectRepository.GetAsync(projectId);

        project = await ProjectManager.RemoveMemberAsync(
            project,
            input.MemberUserId);

        await ProjectRepository.UpdateAsync(project);
    }
}
