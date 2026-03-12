using WorkiomProjectManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace WorkiomProjectManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WorkiomProjectManagementEntityFrameworkCoreModule),
    typeof(WorkiomProjectManagementApplicationContractsModule)
)]
public class WorkiomProjectManagementDbMigratorModule : AbpModule
{
}
