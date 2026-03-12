namespace WorkiomProjectManagement.ProjectManagement;

public static class ProjectMemberConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";
    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "ProjectMember." : string.Empty);
    }
}
