using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using WorkiomProjectManagement.ProjectManagement.Dtos;

namespace WorkiomProjectManagement.ProjectManagement;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectTaskMapper : MapperBase<ProjectTask, ProjectTaskDto>
{
    public override partial ProjectTaskDto Map(ProjectTask source);

    public override partial void Map(ProjectTask source, ProjectTaskDto destination);
}