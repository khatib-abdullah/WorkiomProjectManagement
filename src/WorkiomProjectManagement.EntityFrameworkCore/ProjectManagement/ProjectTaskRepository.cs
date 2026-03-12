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

public class ProjectTaskRepository : EfCoreRepository<WorkiomProjectManagementDbContext, ProjectTask, Guid>, IProjectTaskRepository
{
    public ProjectTaskRepository(IDbContextProvider<WorkiomProjectManagementDbContext> dbContextProvider) : base(dbContextProvider) { }

    public virtual async Task<List<ProjectTask>> GetListByProjectAsync(
        Guid projectId,
        int skipCount,
        int maxResultCount,
        string? sorting,
        TaskStatus? status = null,
        TaskPriority? taskPriority = null,
        Guid? assignedUserId = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var query = includeDetails
            ? await WithDetailsAsync()
            : await GetQueryableAsync();

        query = query
            .Where(x => x.ProjectId == projectId)
            .WhereIf(status.HasValue, x => x.Status == status)
            .WhereIf(taskPriority.HasValue, x => x.Priority == taskPriority)
            .WhereIf(assignedUserId.HasValue && assignedUserId.Value != Guid.Empty, x => x.AssignedUserId == assignedUserId)
            .WhereIf(assignedUserId.HasValue && assignedUserId.Value == Guid.Empty, x => x.AssignedUserId == null);

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
        TaskStatus? status = null,
        TaskPriority? taskPriority = null,
        Guid? assignedUserId = null,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();

        query = query
            .Where(x => x.ProjectId == projectId)
            .WhereIf(status.HasValue, x => x.Status == status)
            .WhereIf(taskPriority.HasValue, x => x.Priority == taskPriority)
            .WhereIf(assignedUserId.HasValue && assignedUserId.Value != Guid.Empty, x => x.AssignedUserId == assignedUserId)
            .WhereIf(assignedUserId.HasValue && assignedUserId.Value == Guid.Empty, x => x.AssignedUserId == null);

        var totalCount = await query
            .LongCountAsync(GetCancellationToken(cancellationToken));

        return totalCount;
    }

    public virtual async Task<List<ProjectTask>> GetListByAssignedUserAsync(
        Guid userId,
        int skipCount,
        int maxResultCount,
        string? sorting,
        Guid? projectId = null,
        TaskStatus? status = null,
        TaskPriority? taskPriority = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        var query = includeDetails
            ? await WithDetailsAsync()
            : await GetQueryableAsync();

        query = query
            .WhereIf(projectId.HasValue, x => x.ProjectId == projectId)
            .Where(x => x.AssignedUserId != null && x.AssignedUserId == userId)
            .WhereIf(status.HasValue, x => x.Status == status)
            .WhereIf(taskPriority.HasValue, x => x.Priority == taskPriority);

        var sortExpression = string.IsNullOrWhiteSpace(sorting)
            ? ProjectTaskConsts.GetDefaultSorting(false)
            : sorting;

        var items = await query
            .OrderBy(sortExpression)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));

        return items;
    }

    public virtual async Task<long> CountByAssignedUserAsync(
        Guid userId,
        Guid? projectId = null,
        TaskStatus? status = null,
        TaskPriority? taskPriority = null,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();

        query = query
            .WhereIf(projectId.HasValue, x => x.ProjectId == projectId)
            .Where(x => x.AssignedUserId != null && x.AssignedUserId == userId)
            .WhereIf(status.HasValue, x => x.Status == status)
            .WhereIf(taskPriority.HasValue, x => x.Priority == taskPriority);

        var totalCount = await query
            .LongCountAsync(GetCancellationToken(cancellationToken));

        return totalCount;
    }

    public virtual async Task DeleteProjectTasksAsync(
        Guid projectId,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();

        query = query
            .Where(x => x.ProjectId == projectId);

        var items = await query
            .ToListAsync(GetCancellationToken(cancellationToken));

        await DeleteManyAsync(items, cancellationToken: GetCancellationToken(cancellationToken));
    }
}
