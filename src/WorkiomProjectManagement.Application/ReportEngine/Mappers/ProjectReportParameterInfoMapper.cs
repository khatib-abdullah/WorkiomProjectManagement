using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using WorkiomProjectManagement.ReportEngine.Dtos;

namespace WorkiomProjectManagement.ReportEngine.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectReportParameterInfoMapper
    : MapperBase<ProjectReportParameterInfo, ProjectReportParameterInfoDto>
{
    public override partial ProjectReportParameterInfoDto Map(ProjectReportParameterInfo source);
    public override partial void Map(ProjectReportParameterInfo source, ProjectReportParameterInfoDto destination);
}