using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectTask : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; private set; }

    public virtual Guid ProjectId { get; private set; }

    public virtual string Title { get; private set; } = null!;
    internal virtual void SetTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), ProjectTaskConsts.MaxTitleLength);
    }

    public virtual string? Description { get; private set; }
    internal virtual void SetDescription(string? description)
    {
        Description = !string.IsNullOrWhiteSpace(description)
            ? Check.NotNullOrWhiteSpace(description, nameof(description), ProjectTaskConsts.MaxDescriptionLength)
            : description;
    }

    public virtual TaskPriority Priority { get; private set; }
    internal virtual void SetPriority(TaskPriority priority)
    {
        Priority = priority;
    }

    public virtual DateOnly? DueDate { get; private set; }
    internal virtual void SetDueDate(DateOnly? dueDate)
    {
        DueDate = dueDate;
    }

    public virtual Guid? AssignedUserId { get; private set; }
    internal virtual void SetAssignedUserId(Guid? newAssignedUserId)
    {
        var oldAssignedUserId = AssignedUserId;

        AssignedUserId = newAssignedUserId;
        RecordAssignmentTransition(oldAssignedUserId, newAssignedUserId);
    }

    private readonly List<ProjectTaskAssignmentHistory> _assignmentHistory = [];
    public virtual IReadOnlyCollection<ProjectTaskAssignmentHistory> AssignmentHistory => _assignmentHistory.AsReadOnly();

    protected virtual void RecordAssignmentTransition(
        Guid? oldAssignedUserId,
        Guid? newAssignedUserId)
    {
        if (oldAssignedUserId == newAssignedUserId)
        {
            return;
        }

        _assignmentHistory.Add(new(Guid.NewGuid(), TenantId, Id, oldAssignedUserId, newAssignedUserId));
    }

    public virtual TaskStatus Status { get; private set; }
    internal virtual void SetStatus(TaskStatus newStatus)
    {
        var oldStatus = Status;

        Status = newStatus;
        RecordStatusTransition(oldStatus, newStatus);
    }

    private readonly List<ProjectTaskStatusHistory> _statusHistory = [];
    public virtual IReadOnlyCollection<ProjectTaskStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    protected virtual void RecordStatusTransition(
        TaskStatus oldStatus,
        TaskStatus newStatus)
    {
        if (oldStatus == newStatus)
        {
            return;
        }

        _statusHistory.Add(new(Guid.NewGuid(), TenantId, Id, oldStatus, newStatus));
    }

    protected ProjectTask() { }
    public ProjectTask(
        Guid id,
        Guid? tenantId,
        Guid projectId,
        string title,
        string? description,
        TaskPriority priority,
        DateOnly? dueDate,
        Guid? assignedUserId)
    {
        Id = id;
        TenantId = tenantId;
        ProjectId = projectId;

        SetTitle(title);
        SetDescription(description);

        SetStatus(TaskStatus.Todo);
        SetPriority(priority);

        SetDueDate(dueDate);
        SetAssignedUserId(assignedUserId);
    }
}
