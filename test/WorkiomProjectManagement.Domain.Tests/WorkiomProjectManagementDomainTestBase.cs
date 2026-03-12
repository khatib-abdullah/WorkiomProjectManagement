using Volo.Abp.Modularity;

namespace WorkiomProjectManagement;

/* Inherit from this class for your domain layer tests. */
public abstract class WorkiomProjectManagementDomainTestBase<TStartupModule> : WorkiomProjectManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
