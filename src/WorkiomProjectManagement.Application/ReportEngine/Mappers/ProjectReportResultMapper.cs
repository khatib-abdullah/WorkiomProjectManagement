using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using WorkiomProjectManagement.ReportEngine.Dtos;

namespace WorkiomProjectManagement.ReportEngine.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectReportResultMapper
    : MapperBase<ProjectReportResult, ProjectReportResultDto>
{
    public override partial ProjectReportResultDto Map(ProjectReportResult source);
    public override partial void Map(ProjectReportResult source, ProjectReportResultDto destination);
}