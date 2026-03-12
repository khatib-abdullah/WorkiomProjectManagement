using Microsoft.Extensions.Localization;
using WorkiomProjectManagement.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace WorkiomProjectManagement;

[Dependency(ReplaceServices = true)]
public class WorkiomProjectManagementBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<WorkiomProjectManagementResource> _localizer;

    public WorkiomProjectManagementBrandingProvider(IStringLocalizer<WorkiomProjectManagementResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
