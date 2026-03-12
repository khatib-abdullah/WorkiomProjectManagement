using Volo.Abp.Domain.Entities;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class UpdateProjectTaskStatusDto : IHasConcurrencyStamp
{
    public TaskStatus Status { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}
