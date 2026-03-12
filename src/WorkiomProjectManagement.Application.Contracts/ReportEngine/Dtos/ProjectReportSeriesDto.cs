using System.Collections.Generic;

namespace WorkiomProjectManagement.ReportEngine.Dtos;

public class ProjectReportSeriesDto
{
    public string Name { get; set; } = null!;
    public List<decimal> Values { get; set; } = [];
}