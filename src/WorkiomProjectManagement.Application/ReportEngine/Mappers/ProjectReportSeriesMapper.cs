using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using WorkiomProjectManagement.ReportEngine.Dtos;

namespace WorkiomProjectManagement.ReportEngine.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectReportSeriesMapper
    : MapperBase<ProjectReportSeries, ProjectReportSeriesDto>
{
    public override partial ProjectReportSeriesDto Map(ProjectReportSeries source);
    public override partial void Map(ProjectReportSeries source, ProjectReportSeriesDto destination);
}
