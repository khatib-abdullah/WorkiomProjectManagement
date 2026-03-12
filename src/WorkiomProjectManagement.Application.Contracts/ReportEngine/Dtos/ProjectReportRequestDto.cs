using System.Collections.Generic;

namespace WorkiomProjectManagement.ReportEngine.Dtos;

public class ProjectReportRequestDto
{
    public Dictionary<string, object?> ExtraProperties { get; set; } = [];
}
