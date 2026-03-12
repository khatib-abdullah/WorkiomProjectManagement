using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class ProjectDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public long MembersCount { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}
