using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WorkiomProjectManagement.ProjectManagement.Dtos;

namespace WorkiomProjectManagement.ProjectManagement;

public interface IProjectAppService : IApplicationService
{
    Task<ProjectDto> GetAsync(Guid id);
    Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectsInput input);

    Task<ProjectDto> CreateAsync(CreateProjectDto input);
    Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectDto input);
    Task DeleteAsync(Guid id);

    Task<ProjectMemberDto> GetMemberAsync(Guid projectId, Guid memberUserId);
    Task<PagedResultDto<ProjectMemberDto>> GetListMembersAsync(Guid projectId, GetProjectMembersInput input);

    Task AddProjectMemberAsync(Guid projectId, AddProjectMemberDto input);
    Task RemoveProjectMemberDtoAsync(Guid projectId, RemoveProjectMemberDto input);
}