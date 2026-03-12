namespace WorkiomProjectManagement.ProjectManagement;

public static class ProjectConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";
    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Project." : string.Empty);
    }

    public const int MaxNameLength = 256;
    public const int MaxDescriptionLength = 2048;
}
