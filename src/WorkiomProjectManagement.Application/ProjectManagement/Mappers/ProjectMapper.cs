using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using WorkiomProjectManagement.ProjectManagement.Dtos;

namespace WorkiomProjectManagement.ProjectManagement;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectMapper : MapperBase<Project, ProjectDto>
{
    public override partial ProjectDto Map(Project source);

    public override partial void Map(Project source, ProjectDto destination);
}
