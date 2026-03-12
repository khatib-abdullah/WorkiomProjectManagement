using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace WorkiomProjectManagement.ProjectManagement;

public class Project : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; private set; }

    public virtual string Name { get; private set; } = null!;
    internal virtual void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProjectConsts.MaxNameLength);
    }

    public virtual string? Description { get; private set; }
    internal virtual void SetDescription(string? description)
    {
        Description = !string.IsNullOrWhiteSpace(description)
            ? Check.NotNullOrWhiteSpace(description, nameof(description), ProjectConsts.MaxDescriptionLength)
            : description;
    }

    private readonly List<ProjectMember> _members = [];
    public virtual IReadOnlyCollection<ProjectMember> Members => _members.AsReadOnly();

    internal virtual void AddMember(
        Guid userId)
    {
        if (_members.Any(x => x.UserId == userId))
        {
            return;
        }

        _members.Add(new(TenantId, Id, userId));
    }

    internal virtual void RemoveMember(
        Guid userId)
    {
        var memberRecord = _members.FirstOrDefault(x => x.UserId == userId);
        if (memberRecord is null)
        {
            return;
        }

        _members.Remove(memberRecord);
    }

    protected Project() { }
    internal Project(
        Guid id,
        Guid? tenantId,
        string name,
        string? description)
    {
        Id = id;
        TenantId = tenantId;

        SetName(name);
        SetDescription(description);
    }
}
