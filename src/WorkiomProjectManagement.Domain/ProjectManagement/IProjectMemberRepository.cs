using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WorkiomProjectManagement.ProjectManagement;

public interface IProjectMemberRepository : IRepository<ProjectMember>
{
    Task<List<ProjectMember>> GetListByProjectAsync(
        Guid projectId,
        int skipCount,
        int maxResultCount,
        string? sorting,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<long> CountByProjectAsync(
        Guid projectId,
        CancellationToken cancellationToken = default);
}
