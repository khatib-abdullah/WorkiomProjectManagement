using System;
using Volo.Abp.Application.Dtos;

namespace WorkiomProjectManagement.ProjectManagement.Dtos;

public class GetProjectTasksInput : PagedAndSortedResultRequestDto
{
    public Guid ProjectId { get; set; }
    public Guid? AssignedUserId { get; set; }
    public TaskStatus? TaskStatus { get; set; }
    public TaskPriority? TaskPriority { get; set; }
}
