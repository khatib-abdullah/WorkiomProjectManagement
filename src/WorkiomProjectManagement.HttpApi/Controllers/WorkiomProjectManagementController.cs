using WorkiomProjectManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace WorkiomProjectManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WorkiomProjectManagementController : AbpControllerBase
{
    protected WorkiomProjectManagementController()
    {
        LocalizationResource = typeof(WorkiomProjectManagementResource);
    }
}
