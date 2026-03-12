using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using WorkiomProjectManagement.EntityFrameworkCore;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectRepository : EfCoreRepository<WorkiomProjectManagementDbContext, Project, Guid>, IProjectRepository
{
    public ProjectRepository(IDbContextProvider<WorkiomProjectManagementDbContext> dbContextProvider) : base(dbContextProvider) { }

    public virtual async Task<bool> IsProjectNameExistsAsync(
        string name, 
        Guid? exceptId = null,
        CancellationToken cancellationToken = default)
    {
        var projectsQuery = await GetQueryableAsync();

        projectsQuery = projectsQuery
            .WhereIf(exceptId.HasValue, x => x.Id != exceptId)
            .Where(x => x.Name == name);

        return await projectsQuery.AnyAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<bool> IsProjectMemberExistsAsync(
        Guid projectId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        var projectMembersQuery = dbContext.Set<ProjectMember>();

        var query = projectMembersQuery
            .Where(x => x.ProjectId == projectId)
            .Where(x => x.UserId == userId);

        return await query.AnyAsync(GetCancellationToken(cancellationToken));
    }
}