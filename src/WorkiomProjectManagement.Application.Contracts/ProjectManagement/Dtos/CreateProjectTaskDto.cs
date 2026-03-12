using System;
using System.ComponentModel.DataAnnotations;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class CreateProjectTaskDto
{
    public Guid ProjectId { get; set; }

    [Required]
    [MaxLength(ProjectTaskConsts.MaxTitleLength)]
    public string Title { get; set; } = null!;

    [MaxLength(ProjectTaskConsts.MaxDescriptionLength)]
    public string? Description { get; set; }

    public TaskPriority Priority { get; set; }
    public DateOnly? DueDate { get; set; }
    public Guid? AssignedUserId { get; set; }
}
