using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectTaskAssignmentHistory : CreationAuditedEntity<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; private set; }

    public virtual Guid ProjectTaskId { get; private set; }
    public virtual Guid? OldAssignedUserId { get; private set; }
    public virtual Guid? NewAssignedUserId { get; private set; }

    protected ProjectTaskAssignmentHistory() { }
    internal ProjectTaskAssignmentHistory(
        Guid id,
        Guid? tenantId,
        Guid projectTaskId,
        Guid? oldAssignedUserId,
        Guid? newAssignedUserId)
    {
        Id = id;
        TenantId = tenantId;

        ProjectTaskId = projectTaskId;
        OldAssignedUserId = oldAssignedUserId;
        NewAssignedUserId = newAssignedUserId;
    }
}
