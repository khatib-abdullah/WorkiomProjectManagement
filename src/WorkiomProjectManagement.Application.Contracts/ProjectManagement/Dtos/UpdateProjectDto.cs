using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class UpdateProjectDto : IHasConcurrencyStamp
{
    [Required]
    [MaxLength(ProjectConsts.MaxNameLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ProjectConsts.MaxDescriptionLength)]
    public string? Description { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}
