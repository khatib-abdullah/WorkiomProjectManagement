using System.Collections.Generic;

namespace WorkiomProjectManagement.ReportEngine;

public class ProjectReportInfo
{
    public string ProjectReportSystemName { get; set; } = null!;

    public List<ProjectReportParameterInfo> Parameters { get; set; } = [];
}
