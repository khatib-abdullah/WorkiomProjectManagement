using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using WorkiomProjectManagement.ReportEngine.Dtos;

namespace WorkiomProjectManagement.ReportEngine.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectReportRequestMapper
    : MapperBase<ProjectReportRequestDto, ProjectReportRequest>
{
    public override partial ProjectReportRequest Map(ProjectReportRequestDto source);
    public override partial void Map(ProjectReportRequestDto source, ProjectReportRequest destination);
}
