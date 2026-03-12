using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using WorkiomProjectManagement.Localization;

namespace WorkiomProjectManagement.ReportEngine;

public abstract class ProjectReportGeneratorBase(IStringLocalizer<WorkiomProjectManagementResource> localizer)
    : IProjectReportGenerator, ITransientDependency
{
    protected IStringLocalizer<WorkiomProjectManagementResource> L { get; } = localizer;

    public abstract string ProjectReportSystemName { get; }
    public abstract IReadOnlyList<ProjectReportParameterInfo> SupportedParameters { get; }
    public abstract Task<ProjectReportResult> GenerateAsync(
        Guid projectId,
        ProjectReportRequest input,
        CancellationToken cancellationToken = default);

    public virtual Task ValidateAsync(
        ProjectReportRequest request,
        CancellationToken cancellationToken = default)
    {
        var errors = new List<string>();

        foreach (var param in SupportedParameters.Where(p => p.Required))
        {
            request.ExtraProperties.TryGetValue(param.Key, out var value);
            if (value == null)
            {
                errors.Add(L["ReportEngine:Error:ParameterRequired", param.Key, ProjectReportSystemName].Value);
            }
        }

        foreach (var param in SupportedParameters)
        {
            request.ExtraProperties.TryGetValue(param.Key, out var value);
            if (value == null)
            {
                continue;
            }

            if (!IsValidType(value, param.Type))
            {
                errors.Add(L["ReportEngine:Error:ParameterInvalidType", param.Key, param.Type].Value);
            }
        }

        if (errors.Any())
        {
            throw new ReportValidationException(ProjectReportSystemName, errors);
        }

        return Task.CompletedTask;
    }

    private static bool IsValidType(object value, string expectedType)
    {
        return expectedType switch
        {
            nameof(Guid) => value is Guid or string
                       && Guid.TryParse(value.ToString(), out _),
            nameof(DateOnly) => value is DateOnly or string
                       && DateOnly.TryParse(value.ToString(), out _),
            nameof(TimeOnly) => value is TimeOnly or string
                       && TimeOnly.TryParse(value.ToString(), out _),
            nameof(DateTime) => value is DateTime or string
                       && DateTime.TryParse(value.ToString(), out _),
            nameof(Boolean) => value is bool or string
                       && bool.TryParse(value.ToString(), out _),
            nameof(Double) => value is double or string
                       && double.TryParse(value.ToString(), out _),
            nameof(Int32) or nameof(Int64) => value is int or long or string
                       && int.TryParse(value.ToString(), out _),
            nameof(String) => value is string,
            _ => true
        };
    }
}
