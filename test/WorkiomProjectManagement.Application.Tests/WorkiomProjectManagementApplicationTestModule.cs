using Volo.Abp.Modularity;

namespace WorkiomProjectManagement;

[DependsOn(
    typeof(WorkiomProjectManagementApplicationModule),
    typeof(WorkiomProjectManagementDomainTestModule)
)]
public class WorkiomProjectManagementApplicationTestModule : AbpModule
{

}
