using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectMember : FullAuditedEntity, IMultiTenant
{
    public virtual Guid? TenantId { get; private set; }

    public virtual Guid ProjectId { get; private set; }
    public virtual Guid UserId { get; private set; }
    public virtual IdentityUser? User { get; private set; }

    protected ProjectMember() { }
    internal ProjectMember(
        Guid? tenantId,
        Guid projectId,
        Guid userId)
    {
        TenantId = tenantId;
        ProjectId = projectId;
        UserId = userId;
    }

    public override object?[] GetKeys()
    {
        return [ProjectId, UserId];
    }
}