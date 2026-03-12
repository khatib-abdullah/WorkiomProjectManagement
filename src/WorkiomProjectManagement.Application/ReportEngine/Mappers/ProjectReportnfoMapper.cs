using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using WorkiomProjectManagement.ReportEngine.Dtos;

namespace WorkiomProjectManagement.ReportEngine.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectReportnfoMapper
    : MapperBase<ProjectReportInfo, ProjectReportInfoDto>
{
    public override partial ProjectReportInfoDto Map(ProjectReportInfo source);
    public override partial void Map(ProjectReportInfo source, ProjectReportInfoDto destination);
}
