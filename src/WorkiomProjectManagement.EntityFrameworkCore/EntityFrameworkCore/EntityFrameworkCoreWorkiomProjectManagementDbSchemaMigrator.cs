using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkiomProjectManagement.Data;
using Volo.Abp.DependencyInjection;

namespace WorkiomProjectManagement.EntityFrameworkCore;

public class EntityFrameworkCoreWorkiomProjectManagementDbSchemaMigrator
    : IWorkiomProjectManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreWorkiomProjectManagementDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the WorkiomProjectManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<WorkiomProjectManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}
