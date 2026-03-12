using System;
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
[Authorize(WorkiomProjectManagementPermissions.ProjectTasks.Default)]
public class ProjectTaskAppService : WorkiomProjectManagementAppService, IProjectTaskAppService
{
    protected virtual ProjectTaskManager ProjectTaskManager => LazyServiceProvider.LazyGetRequiredService<ProjectTaskManager>();
    protected virtual IProjectTaskRepository ProjectTaskRepository => LazyServiceProvider.LazyGetRequiredService<IProjectTaskRepository>();

    public virtual async Task<ProjectTaskDto> GetAsync(Guid id)
    {
        var projectTask = await ProjectTaskRepository.GetAsync(id);
        return ObjectMapper.Map<ProjectTask, ProjectTaskDto>(projectTask);
    }

    public virtual async Task<PagedResultDto<ProjectTaskDto>> GetListAsync(GetProjectTasksInput input)
    {
        var totalCount = await ProjectTaskRepository.CountByProjectAsync(
            input.ProjectId,
            input.TaskStatus,
            input.TaskPriority,
            input.AssignedUserId
        );

        var projectTasks = await ProjectTaskRepository.GetListByProjectAsync(
            input.ProjectId,
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting ?? ProjectTaskConsts.GetDefaultSorting(false),
            input.TaskStatus,
            input.TaskPriority,
            input.AssignedUserId
        );

        return new PagedResultDto<ProjectTaskDto>(
            totalCount,
            [.. projectTasks.Select(ObjectMapper.Map<ProjectTask, ProjectTaskDto>)]
        );
    }

    [Authorize(WorkiomProjectManagementPermissions.ProjectTasks.Create)]
    public virtual async Task<ProjectTaskDto> CreateAsync(CreateProjectTaskDto input)
    {
        var projectTask = await ProjectTaskManager.CreateAsync(
            input.ProjectId,
            input.Title,
            input.Description,
            input.Priority,
            input.DueDate,
            input.AssignedUserId);

        await ProjectTaskRepository.InsertAsync(projectTask);
        return ObjectMapper.Map<ProjectTask, ProjectTaskDto>(projectTask);
    }

    [Authorize(WorkiomProjectManagementPermissions.ProjectTasks.Edit)]
    public virtual async Task<ProjectTaskDto> UpdateAsync(Guid id, UpdateProjectTaskDto input)
    {
        var projectTask = await ProjectTaskRepository.GetAsync(id);

        projectTask = await ProjectTaskManager.UpdateAsync(
            projectTask,
            input.Title,
            input.Description,
            input.Status,
            input.Priority,
            input.DueDate,
            input.AssignedUserId);

        projectTask.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        await ProjectTaskRepository.UpdateAsync(projectTask);
        return ObjectMapper.Map<ProjectTask, ProjectTaskDto>(projectTask);
    }

    [Authorize(WorkiomProjectManagementPermissions.ProjectTasks.Edit)]
    public virtual async Task<ProjectTaskDto> UpdateProjectTaskAssignmentDtoAsync(Guid id, UpdateProjectTaskAssignmentDto input)
    {
        var projectTask = await ProjectTaskRepository.GetAsync(id);

        projectTask = await ProjectTaskManager.UpdateAssignedUserIdAsync(
            projectTask,
            input.AssignedUserId);

        projectTask.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        await ProjectTaskRepository.UpdateAsync(projectTask);
        return ObjectMapper.Map<ProjectTask, ProjectTaskDto>(projectTask);
    }

    [Authorize(WorkiomProjectManagementPermissions.ProjectTasks.Edit)]
    public virtual async Task<ProjectTaskDto> UpdateProjectTaskStatusAsync(Guid id, UpdateProjectTaskStatusDto input)
    {
        var projectTask = await ProjectTaskRepository.GetAsync(id);

        projectTask = await ProjectTaskManager.UpdateStatusAsync(
            projectTask,
            input.Status);

        projectTask.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        await ProjectTaskRepository.UpdateAsync(projectTask);
        return ObjectMapper.Map<ProjectTask, ProjectTaskDto>(projectTask);
    }

    [Authorize(WorkiomProjectManagementPermissions.ProjectTasks.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        var projectTask = await ProjectTaskRepository.GetAsync(id);

        await ProjectTaskRepository.DeleteAsync(projectTask);
    }
}
