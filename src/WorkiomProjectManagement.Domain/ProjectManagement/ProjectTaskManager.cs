using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectTaskManager : DomainService
{
    public IProjectRepository ProjectRepository => LazyServiceProvider.LazyGetRequiredService<IProjectRepository>();

    public virtual async Task<ProjectTask> CreateAsync(
        Guid projectId,
        string title,
        string? description,
        TaskPriority priority,
        DateOnly? dueDate,
        Guid? assignedUserId)
    {
        if (assignedUserId.HasValue && !await ProjectRepository.IsProjectMemberExistsAsync(projectId, assignedUserId.Value))
        {
            throw new BusinessException(WorkiomProjectManagementDomainErrorCodes.ProjectTaskAssignedUserNotAProjectMember);
        }

        return new(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            projectId,
            title,
            description,
            priority,
            dueDate,
            assignedUserId);
    }

    public virtual async Task<ProjectTask> UpdateAsync(
        ProjectTask projectTask,
        string title,
        string? description,
        TaskStatus taskStatus,
        TaskPriority taskPriority,
        DateOnly? dueDate,
        Guid? assignedUserId)
    {
        projectTask.SetTitle(title);
        projectTask.SetDescription(description);
        projectTask.SetPriority(taskPriority);
        projectTask.SetDueDate(dueDate);

        await UpdateStatusAsync(projectTask, taskStatus);
        await UpdateAssignedUserIdAsync(projectTask, assignedUserId);

        return projectTask;
    }

    public virtual async Task<ProjectTask> UpdateStatusAsync(
        ProjectTask projectTask,
        TaskStatus taskStatus)
    {
        projectTask.SetStatus(taskStatus);

        return projectTask;
    }

    public virtual async Task<ProjectTask> UpdateAssignedUserIdAsync(
        ProjectTask projectTask,
        Guid? assignedUserId)
    {
        if (assignedUserId.HasValue && !await ProjectRepository.IsProjectMemberExistsAsync(projectTask.ProjectId, assignedUserId.Value))
        {
            throw new BusinessException(WorkiomProjectManagementDomainErrorCodes.ProjectTaskAssignedUserNotAProjectMember);
        }

        projectTask.SetAssignedUserId(assignedUserId);

        return projectTask;
    }
}