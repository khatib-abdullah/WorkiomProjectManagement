using System.Threading.Tasks;

namespace WorkiomProjectManagement.Data;

public interface IWorkiomProjectManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
