using System.ComponentModel.DataAnnotations;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class CreateProjectDto
{
    [Required]
    [MaxLength(ProjectConsts.MaxNameLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ProjectConsts.MaxDescriptionLength)]
    public string? Description { get; set; }
}