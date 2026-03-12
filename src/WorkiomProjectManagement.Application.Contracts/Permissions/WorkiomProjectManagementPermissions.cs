namespace WorkiomProjectManagement.Permissions;

public static class WorkiomProjectManagementPermissions
{
    public const string GroupName = "WorkiomProjectManagement";

    public static class Projects
    {
        public const string Default = GroupName + ".Projects";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string ManageMembers = Default + ".ManageMembers";
        public const string ManageReports = Default + ".ManageReports";
    }

    public static class ProjectTasks
    {
        public const string Default = GroupName + ".ProjectTasks";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
