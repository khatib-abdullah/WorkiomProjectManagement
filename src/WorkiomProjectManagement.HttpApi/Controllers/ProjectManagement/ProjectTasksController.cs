using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using WorkiomProjectManagement.ProjectManagement;
using WorkiomProjectManagement.ProjectManagement.Dtos;

namespace WorkiomProjectManagement.Controllers.ProjectManagement;

[Area("app")]
[Route("api/app/project-tasks")]
public class ProjectTasksController : WorkiomProjectManagementController, IProjectTaskAppService
{
    protected IProjectTaskAppService ProjectTaskAppService => LazyServiceProvider.LazyGetRequiredService<IProjectTaskAppService>();

    [HttpGet("{id}")]
    public virtual Task<ProjectTaskDto> GetAsync(Guid id)
    {
        return ProjectTaskAppService.GetAsync(id);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<ProjectTaskDto>> GetListAsync(GetProjectTasksInput input)
    {
        return ProjectTaskAppService.GetListAsync(input);
    }

    [HttpPost]
    public virtual Task<ProjectTaskDto> CreateAsync(CreateProjectTaskDto input)
    {
        return ProjectTaskAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public virtual Task<ProjectTaskDto> UpdateAsync(Guid id, UpdateProjectTaskDto input)
    {
        return ProjectTaskAppService.UpdateAsync(id, input);
    }

    [HttpPut("{id}/assignment")]
    public virtual Task<ProjectTaskDto> UpdateProjectTaskAssignmentDtoAsync(Guid id, UpdateProjectTaskAssignmentDto input)
    {
        return ProjectTaskAppService.UpdateProjectTaskAssignmentDtoAsync(id, input);
    }

    [HttpPut("{id}/status")]
    public virtual Task<ProjectTaskDto> UpdateProjectTaskStatusAsync(Guid id, UpdateProjectTaskStatusDto input)
    {
        return ProjectTaskAppService.UpdateProjectTaskStatusAsync(id, input);
    }

    [HttpDelete("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return ProjectTaskAppService.DeleteAsync(id);
    }
}