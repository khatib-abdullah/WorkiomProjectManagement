using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WorkiomProjectManagement.ProjectManagement.Dtos;

namespace WorkiomProjectManagement.ProjectManagement;

public interface IProjectTaskAppService : IApplicationService
{
    Task<ProjectTaskDto> GetAsync(Guid id);
    Task<PagedResultDto<ProjectTaskDto>> GetListAsync(GetProjectTasksInput input);
    Task<ProjectTaskDto> CreateAsync(CreateProjectTaskDto input);
    Task<ProjectTaskDto> UpdateAsync(Guid id, UpdateProjectTaskDto input);
    Task<ProjectTaskDto> UpdateProjectTaskStatusAsync(Guid id, UpdateProjectTaskStatusDto input);
    Task<ProjectTaskDto> UpdateProjectTaskAssignmentDtoAsync(Guid id, UpdateProjectTaskAssignmentDto input);
    Task DeleteAsync(Guid id);
}
