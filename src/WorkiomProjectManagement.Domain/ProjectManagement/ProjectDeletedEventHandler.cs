using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace WorkiomProjectManagement.ProjectManagement;

public class ProjectDeletedEventHandler : ILocalEventHandler<EntityDeletedEventData<Project>>, ITransientDependency
{
    private readonly IProjectTaskRepository _projectTaskRepository;
    private readonly IProjectMemberRepository _projectMemberRepository;

    public ProjectDeletedEventHandler(
        IProjectTaskRepository projectTaskRepository,
        IProjectMemberRepository projectMemberRepository)
    {
        _projectTaskRepository = projectTaskRepository;
        _projectMemberRepository = projectMemberRepository;
    }

    public async Task HandleEventAsync(EntityDeletedEventData<Project> eventData)
    {
        await _projectMemberRepository.DeleteManyAsync(eventData.Entity.Members);
        await _projectTaskRepository.DeleteProjectTasksAsync(eventData.Entity.Id);
    }
}