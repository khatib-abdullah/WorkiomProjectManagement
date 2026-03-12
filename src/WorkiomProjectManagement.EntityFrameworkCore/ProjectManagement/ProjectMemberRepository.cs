using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WorkiomProjectManagement.EntityFrameworkCore;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectMemberRepository : EfCoreRepository<WorkiomProjectManagementDbContext, ProjectMember>, IProjectMemberRepository
{
    public ProjectMemberRepository(IDbContextProvider<WorkiomProjectManagementDbContext> dbContextProvider) : base(dbContextProvider) { }

    public virtual async Task<List<ProjectMember>> GetListByProjectAsync(
        Guid projectId, 
        int skipCount, 
        int maxResultCount, 
        string? sorting, 
        bool includeDetails = false, 
        CancellationToken cancellationToken = default)
    {
        var query = includeDetails
            ? await WithDetailsAsync()
            : await GetQueryableAsync();

        query = query
            .Where(x => x.ProjectId == projectId);

        var sortExpression = string.IsNullOrWhiteSpace(sorting)
            ? ProjectTaskConsts.GetDefaultSorting(false)
            : sorting;

        var items = await query
            .OrderBy(sortExpression)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));

        return items;
    }

    public virtual async Task<long> CountByProjectAsync(
        Guid projectId, 
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();

        query = query
            .Where(x => x.ProjectId == projectId);

        var totalCount = await query
            .LongCountAsync(GetCancellationToken(cancellationToken));

        return totalCount;
    }
}