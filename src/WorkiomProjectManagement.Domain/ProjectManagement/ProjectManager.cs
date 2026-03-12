using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectManager : DomainService
{
    public IProjectRepository ProjectRepository => LazyServiceProvider.LazyGetRequiredService<IProjectRepository>();

    public virtual async Task<Project> CreateProjectAsync(
        string name,
        string? description)
    {
        if (await ProjectRepository.IsProjectNameExistsAsync(name))
        {
            throw new BusinessException(WorkiomProjectManagementDomainErrorCodes.ProjectNameAlreadyExists);
        }

        return new(
            GuidGenerator.Create(), 
            CurrentTenant.Id,
            name,
            description);
    }

    public virtual async Task<Project> UpdateProjectAsync(
        Project project,
        string name,
        string? description)
    {
        if (!project.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && await ProjectRepository.IsProjectNameExistsAsync(name, project.Id))
        {
            throw new BusinessException(WorkiomProjectManagementDomainErrorCodes.ProjectNameAlreadyExists);
        }

        project.SetName(name);
        project.SetDescription(description);

        return project;
    }

    public virtual async Task<Project> AddMemberAsync(
        Project project,
        Guid userId)
    {
        if (project.Members.Any(x => x.UserId == userId))
        {
            throw new BusinessException(WorkiomProjectManagementDomainErrorCodes.ProjectMemberAlreadyExists);
        }

        project.AddMember(userId);

        return project;
    }

    public virtual async Task<Project> RemoveMemberAsync(
        Project project,
        Guid userId)
    {
        if (!project.Members.Any(x => x.UserId == userId))
        {
            throw new BusinessException(WorkiomProjectManagementDomainErrorCodes.ProjectMemberNotExists);
        }

        project.RemoveMember(userId);

        return project;
    }
}
