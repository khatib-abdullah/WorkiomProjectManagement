using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace WorkiomProjectManagement.Data;

/* This is used if database provider does't define
 * IWorkiomProjectManagementDbSchemaMigrator implementation.
 */
public class NullWorkiomProjectManagementDbSchemaMigrator : IWorkiomProjectManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
