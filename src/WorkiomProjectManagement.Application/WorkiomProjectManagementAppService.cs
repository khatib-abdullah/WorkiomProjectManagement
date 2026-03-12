using WorkiomProjectManagement.Localization;
using Volo.Abp.Application.Services;

namespace WorkiomProjectManagement;

/* Inherit your application services from this class.
 */
public abstract class WorkiomProjectManagementAppService : ApplicationService
{
    protected WorkiomProjectManagementAppService()
    {
        LocalizationResource = typeof(WorkiomProjectManagementResource);
    }
}
