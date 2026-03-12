using Volo.Abp.Modularity;

namespace WorkiomProjectManagement;

[DependsOn(
    typeof(WorkiomProjectManagementDomainModule),
    typeof(WorkiomProjectManagementTestBaseModule)
)]
public class WorkiomProjectManagementDomainTestModule : AbpModule
{

}
