using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WorkiomProjectManagement.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class WorkiomProjectManagementDbContextFactory : IDesignTimeDbContextFactory<WorkiomProjectManagementDbContext>
{
    public WorkiomProjectManagementDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        var configuration = BuildConfiguration();
        
        WorkiomProjectManagementEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<WorkiomProjectManagementDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));
        
        return new WorkiomProjectManagementDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WorkiomProjectManagement.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
