using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WorkiomProjectManagement.ProjectManagement;

public interface IProjectRepository : IRepository<Project, Guid>
{
    Task<bool> IsProjectNameExistsAsync(
        string name, 
        Guid? exceptId = null,
        CancellationToken cancellationToken = default);

    Task<bool> IsProjectMemberExistsAsync(
        Guid projectId,
        Guid userId,
        CancellationToken cancellationToken = default);
}