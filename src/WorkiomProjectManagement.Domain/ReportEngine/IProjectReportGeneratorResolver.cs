using System.Collections.Generic;

namespace WorkiomProjectManagement.ReportEngine;

public interface IProjectReportGeneratorResolver
{
    IProjectReportGenerator Resolve(string projectReportSystemName);
    IReadOnlyList<ProjectReportInfo> GetAvailableReportsInfo();
}