# Architecture

## Layer Structure

This solution follows a **Domain-Driven Design (DDD)** layered architecture built on the [ABP Framework](https://abp.io).

```
src/
├── WorkiomProjectManagement.Domain.Shared          # Enums, constants, localization keys
├── WorkiomProjectManagement.Domain                 # Entities, domain managers, repository interfaces
├── WorkiomProjectManagement.Application.Contracts  # Service interfaces, DTOs, permission definitions
├── WorkiomProjectManagement.Application            # Application services, object mappers
├── WorkiomProjectManagement.EntityFrameworkCore    # EF Core context, repository implementations, migrations
├── WorkiomProjectManagement.HttpApi                # HTTP controllers
├── WorkiomProjectManagement.HttpApi.Host           # ASP.NET Core host, startup, middleware
└── WorkiomProjectManagement.DbMigrator             # Database migration runner
```

### Domain (`Domain` + `Domain.Shared`)

Contains the core business model with no external dependencies.

- **Aggregates:** `Project`, `ProjectTask` — both are fully audited aggregate roots with multi-tenancy support
- **Entities:** `ProjectMember`, `ProjectTaskStatusHistory`, `ProjectTaskAssignmentHistory`
- **Domain Managers:** `ProjectManager`, `ProjectTaskManager` — enforce business rules (e.g. name uniqueness, member validation before task assignment)
- **Repository Interfaces:** `IProjectRepository`, `IProjectTaskRepository`, `IProjectMemberRepository`
- **Report Engine:** `IProjectReportGenerator`, `ProjectReportGeneratorBase`, `ProjectReportGeneratorResolver` — pluggable report type system

### Application Contracts (`Application.Contracts`)

Public API surface shared between the application and HTTP layers.

- Service interfaces: `IProjectAppService`, `IProjectTaskAppService`, `IProjectReportAppService`
- Input/output DTOs for all operations
- Permission constants via `WorkiomProjectManagementPermissions`

### Application (`Application`)

Implements service interfaces. Coordinates domain managers, repositories, and object mapping. No direct EF Core dependency.

- `ProjectAppService` — project CRUD, member management, task count aggregation
- `ProjectTaskAppService` — task CRUD, status/assignment updates with filter support
- `ProjectReportAppService` — delegates to the report engine resolver
- Object mapping via [Mapperly](https://github.com/riok/mapperly) source-generated mappers

### EntityFrameworkCore (`EntityFrameworkCore`)

Data access layer. Contains the only EF Core dependency in the solution.

- `WorkiomProjectManagementDbContext` — single context for all entities, replaces ABP Identity and TenantManagement contexts
- Repository implementations: `ProjectRepository`, `ProjectTaskRepository`, `ProjectMemberRepository`
- Report generator implementations live here to keep EF Core queries out of the domain

### HttpApi (`HttpApi` + `HttpApi.Host`)

- `ProjectsController`, `ProjectTasksController` — thin controllers that delegate directly to application services
- Host configures OpenIddict authentication, Swagger, CORS, health checks, Serilog, and Autofac

---

## Report Engine

The report engine is designed to be **open for extension, closed for modification**.

- `IProjectReportGenerator` defines the contract: a system name, a parameter schema, and a `GenerateAsync` method
- `ProjectReportGeneratorBase` provides parameter validation, shared localization, and ABP `ITransientDependency` registration
- `ProjectReportGeneratorResolver` auto-discovers all registered generators via constructor injection — no switch statements or factory registrations needed
- Adding a new report type requires creating **one new class** that extends `ProjectReportGeneratorBase`. Nothing else changes.

Built-in report types:
| System Name | Description | Author |
|---|---|---|
| `project_tasks_by_status` | Task count grouped by status | Hand-written |
| `project_tasks_by_priority` | Task count grouped by priority | Hand-written |
| `project_tasks_overdue_by_assignee` | Overdue task count grouped by assignee | AI-generated (see AI Extension) |

---

## AI Extension

### Prompt

> I'm working on a .NET / ABP Framework project. I need you to add a **new report type** to the existing report engine. Do not modify any existing files.
>
> **Before writing anything**, read these classes to understand the pattern:
> - `ProjectReportGeneratorBase`
> - `IProjectReportGenerator`
> - `ProjectReportResult`
> - `ProjectReportRequest`
> - `ProjectTask`
>
> Then read **both existing generators** in their entirety:
> - `ProjectTasksByStatusReportGenerator`
> - `ProjectTasksByPriorityReportGenerator`
>
> **Task:** Create a single new class: `ProjectTasksOverdueByAssigneeReportGenerator`
>
> The generator must:
> - Use system name `"project_tasks_overdue_by_assignee"`
> - Query tasks for the given `projectId` where `DueDate` is in the past and `Status` is not `Done` or `Cancelled`
> - Group results by `AssignedUserId`, using `"Unassigned"` as the label when `AssignedUserId` is null
> - Support one optional parameter: `status` (`TaskStatus`, optional) to further filter by a specific status
> - Return one series named `"Overdue Tasks"` with a count per assignee
> - Follow the exact same structure, style, and injection pattern as the existing generators
>
> Also read `en.json` (the localization file) and add any new keys your generator uses, following the existing key naming conventions.

### What the AI got right

The AI correctly read and replicated the full structure of the existing generators: constructor injection of `IProjectTaskRepository` and `IStringLocalizer`, the `SupportedParameters` readonly list pattern, use of `GetProperty<T>()` to read typed parameters, `WhereIf` for optional filters, and the `ProjectReportResult` shape with `Labels` and `Series`. It correctly used `DateOnly.FromDateTime(DateTime.UtcNow)` as a local variable before the query (not inline), which EF Core can translate.

### What needed fixing

The AI did not add the localization keys it introduced (`Report:Series:OverdueTasks`, `Report:Label:Unassigned`) to `en.json`, despite the prompt explicitly asking it to. These were added manually.
