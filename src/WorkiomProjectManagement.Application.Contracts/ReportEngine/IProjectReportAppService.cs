using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkiomProjectManagement.ReportEngine.Dtos;

namespace WorkiomProjectManagement.ReportEngine;

public interface IProjectReportAppService
{
    Task<ProjectReportResultDto> GenerateProjectReportAsync(Guid projectId, string projectReportSystemName, ProjectReportRequestDto input);

    Task<List<ProjectReportInfoDto>> GetAvailableProjectReportTypesAsync();
}
