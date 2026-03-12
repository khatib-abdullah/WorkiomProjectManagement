using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class ProjectTaskDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid ProjectId { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateOnly? DueDate { get; set; }
    public Guid? AssignedUserId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}