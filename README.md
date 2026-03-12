# WorkiomProjectManagement

A Task & Project Management REST API built with .NET 10 and the ABP Framework.

## Running the application

Requires [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet) and a running PostgreSQL instance.

The default connection string points to `localhost:5432` with database `WorkiomProjectManagement`, user `postgres`, password `12345678`. Update `appsettings.json` under `WorkiomProjectManagement.HttpApi.Host` and `WorkiomProjectManagement.DbMigrator` if needed.

```bash
# Run migrations
dotnet run --project src/WorkiomProjectManagement.DbMigrator

# Start the API
dotnet run --project src/WorkiomProjectManagement.HttpApi.Host
```

The API will be available at `https://localhost:44355`. Swagger UI is at `https://localhost:44355/swagger`.

---

## Design decisions

### DDD layered architecture over ABP

The solution uses ABP's layered DDD template. Domain logic lives exclusively in `Domain` — entities, managers, and repository interfaces. `EntityFrameworkCore` holds the only EF Core dependency. Application services coordinate between the two without leaking persistence concerns upward.

### Report engine extensibility

The report engine uses a plugin pattern: each report type is an independent class that extends `ProjectReportGeneratorBase` and is auto-discovered via ABP's DI. Adding a new report type requires creating one class — no registration, no factory changes, no engine modifications. See `ARCHITECTURE.md` for details.

### Task count moved to the task repository

`GetCountsPerProjectAsync` lives on `IProjectTaskRepository`, not `IProjectRepository`. Task counts are the task repository's responsibility. `ProjectAppService` calls both repositories and merges the results, keeping each repository focused on its own aggregate.

### History tracking on tasks

`ProjectTask` maintains `StatusHistory` and `AssignmentHistory` as owned collections. Every status change and reassignment is recorded automatically by the domain manager, providing a full audit trail without additional infrastructure.

### Multi-tenancy

All entities implement `IMultiTenant`. The ABP data filter ensures tenants are always isolated at the query level without any manual `WHERE TenantId = ?` clauses in application code.
