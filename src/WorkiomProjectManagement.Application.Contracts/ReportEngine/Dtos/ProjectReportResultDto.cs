using System.Collections.Generic;

namespace WorkiomProjectManagement.ReportEngine.Dtos;

public class ProjectReportResultDto
{
    public string ProjectReportSystemName { get; set; } = null!;

    public List<string> Labels { get; set; } = [];
    public List<ProjectReportSeriesDto> Series { get; set; } = [];
}
