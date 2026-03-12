using System.Collections.Generic;

namespace WorkiomProjectManagement.ReportEngine;

public class ProjectReportResult
{
    public string ProjectReportSystemName { get; set; } = null!;

    public List<string> Labels { get; set; } = [];
    public List<ProjectReportSeries> Series { get; set; } = [];
}