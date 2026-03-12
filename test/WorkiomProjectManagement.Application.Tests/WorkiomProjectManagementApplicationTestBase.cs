using Volo.Abp.Modularity;

namespace WorkiomProjectManagement;

public abstract class WorkiomProjectManagementApplicationTestBase<TStartupModule> : WorkiomProjectManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
