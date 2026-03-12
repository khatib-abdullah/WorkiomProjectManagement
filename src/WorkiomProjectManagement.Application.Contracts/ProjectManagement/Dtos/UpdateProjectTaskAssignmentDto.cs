using System;
using Volo.Abp.Domain.Entities;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class UpdateProjectTaskAssignmentDto : IHasConcurrencyStamp
{
    public Guid? AssignedUserId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}