using System.Collections.Generic;

namespace WorkiomProjectManagement.ReportEngine.Dtos;

public class ProjectReportInfoDto
{
    public string ProjectReportSystemName { get; set; } = default!;
    public List<ProjectReportParameterInfoDto> Parameters { get; set; } = [];
}
