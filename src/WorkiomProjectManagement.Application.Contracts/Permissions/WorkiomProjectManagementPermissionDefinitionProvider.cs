using WorkiomProjectManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace WorkiomProjectManagement.Permissions;

public class WorkiomProjectManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WorkiomProjectManagementPermissions.GroupName);

        var projectsPermission = myGroup.AddPermission(WorkiomProjectManagementPermissions.Projects.Default, L("Permission:Projects"));
        projectsPermission.AddChild(WorkiomProjectManagementPermissions.Projects.Create, L("Permission:Projects.Create"));
        projectsPermission.AddChild(WorkiomProjectManagementPermissions.Projects.Edit, L("Permission:Projects.Edit"));
        projectsPermission.AddChild(WorkiomProjectManagementPermissions.Projects.Delete, L("Permission:Projects.Delete"));
        projectsPermission.AddChild(WorkiomProjectManagementPermissions.Projects.ManageMembers, L("Permission:Projects.ManageMembers"));
        projectsPermission.AddChild(WorkiomProjectManagementPermissions.Projects.ManageReports, L("Permission:Projects.ManageReports"));

        var projectTasksPermission = myGroup.AddPermission(WorkiomProjectManagementPermissions.ProjectTasks.Default, L("Permission:ProjectTasks"));
        projectTasksPermission.AddChild(WorkiomProjectManagementPermissions.ProjectTasks.Create, L("Permission:ProjectTasks.Create"));
        projectTasksPermission.AddChild(WorkiomProjectManagementPermissions.ProjectTasks.Edit, L("Permission:ProjectTasks.Edit"));
        projectTasksPermission.AddChild(WorkiomProjectManagementPermissions.ProjectTasks.Delete, L("Permission:ProjectTasks.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WorkiomProjectManagementResource>(name);
    }
}
