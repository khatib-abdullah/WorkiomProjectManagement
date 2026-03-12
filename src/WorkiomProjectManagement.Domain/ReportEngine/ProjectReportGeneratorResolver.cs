using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace WorkiomProjectManagement.ReportEngine;

public class ProjectReportGeneratorResolver : IProjectReportGeneratorResolver, ITransientDependency
{
    private readonly Dictionary<string, IProjectReportGenerator> _generators;

    public ProjectReportGeneratorResolver(IEnumerable<IProjectReportGenerator> generators)
    {
        _generators = generators
            .ToDictionary(x => x.ProjectReportSystemName, x => x);
    }

    public virtual IProjectReportGenerator Resolve(string projectReportSystemName)
    {
        if (!_generators.TryGetValue(projectReportSystemName, out var generator))
        {
            throw new BusinessException(WorkiomProjectManagementDomainErrorCodes.ReportEngineUnsupportedReportType)
                .WithData("projectReportSystemName", projectReportSystemName);
        }

        return generator;
    }

    public virtual IReadOnlyList<ProjectReportInfo> GetAvailableReportsInfo()
    {
        var projectReportsInfo = _generators
            .Select(x => new ProjectReportInfo()
            {
                ProjectReportSystemName = x.Key,
                Parameters = [.. x.Value.SupportedParameters]
            })
            .ToList();

        return projectReportsInfo;
    }
}