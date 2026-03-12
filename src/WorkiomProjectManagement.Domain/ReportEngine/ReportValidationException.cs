using System.Collections.Generic;
using Volo.Abp;

namespace WorkiomProjectManagement.ReportEngine;

public class ReportValidationException : BusinessException
{
    public List<string> Errors { get; }

    public ReportValidationException(
        string projectReportSystemName,
        List<string> errors)
        : base(WorkiomProjectManagementDomainErrorCodes.ReportEngineInvalidReportParameters)
    {
        Errors = errors;

        WithData("projectReportSystemName", projectReportSystemName);
        WithData("errors", errors);
    }
}