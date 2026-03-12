using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WorkiomProjectManagement.ReportEngine;

public interface IProjectReportGenerator
{
    string ProjectReportSystemName { get; }
    IReadOnlyList<ProjectReportParameterInfo> SupportedParameters { get; }

    Task ValidateAsync(
        ProjectReportRequest input,
        CancellationToken cancellationToken = default);

    Task<ProjectReportResult> GenerateAsync(
        Guid projectId,
        ProjectReportRequest input,
        CancellationToken cancellationToken = default);
}
