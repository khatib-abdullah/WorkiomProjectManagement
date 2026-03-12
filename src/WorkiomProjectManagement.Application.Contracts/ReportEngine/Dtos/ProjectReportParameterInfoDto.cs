namespace WorkiomProjectManagement.ReportEngine.Dtos;

public class ProjectReportParameterInfoDto
{
    public string Key { get; set; } = default!;
    public string Type { get; set; } = default!;
    public bool Required { get; set; }
}