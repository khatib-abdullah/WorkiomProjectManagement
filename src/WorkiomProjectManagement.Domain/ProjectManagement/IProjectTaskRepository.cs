using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WorkiomProjectManagement.ProjectManagement;

public interface IProjectTaskRepository : IRepository<ProjectTask, Guid>
{
    Task<List<ProjectTask>> GetListByProjectAsync(
        Guid projectId,
        int skipCount,
        int maxResultCount,
        string? sorting,
        TaskStatus? status = null,
        TaskPriority? taskPriority = null,
        Guid? assignedUserId = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<long> CountByProjectAsync(
        Guid projectId,
        TaskStatus? status = null,
        TaskPriority? taskPriority = null,
        Guid? assignedUserId = null,
        CancellationToken cancellationToken = default);

    Task<List<ProjectTask>> GetListByAssignedUserAsync(
        Guid assignedUserId,
        int skipCount,
        int maxResultCount,
        string? sorting,
        Guid? projectId = null,
        TaskStatus? status = null,
        TaskPriority? taskPriority = null,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<long> CountByAssignedUserAsync(
        Guid assignedUserId,
        Guid? projectId = null,
        TaskStatus? status = null,
        TaskPriority? taskPriority = null,
        CancellationToken cancellationToken = default);

    Task<Dictionary<Guid, long>> GetCountsPerProjectAsync(
        IEnumerable<Guid> projectIds,
        CancellationToken cancellationToken = default);

    Task DeleteProjectTasksAsync(
        Guid projectId,
        CancellationToken cancellationToken = default);
}