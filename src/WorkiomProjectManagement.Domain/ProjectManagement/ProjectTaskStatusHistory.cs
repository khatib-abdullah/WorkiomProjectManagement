using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectTaskStatusHistory : CreationAuditedEntity<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; private set; }

    public virtual Guid ProjectTaskId { get; private set; }
    public virtual TaskStatus OldStatus { get; private set; }
    public virtual TaskStatus NewStatus { get; private set; }

    protected ProjectTaskStatusHistory() { }
    internal ProjectTaskStatusHistory(
        Guid id,
        Guid? tenantId,
        Guid projectTaskId,
        TaskStatus oldStatus,
        TaskStatus newStatus)
    {
        Id = id;
        TenantId = tenantId;

        ProjectTaskId = projectTaskId;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }
}
