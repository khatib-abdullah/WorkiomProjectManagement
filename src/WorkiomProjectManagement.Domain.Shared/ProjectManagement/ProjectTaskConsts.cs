namespace WorkiomProjectManagement.ProjectManagement;

public static class ProjectTaskConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";
    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "ProjectTask." : string.Empty);
    }

    public const int MaxTitleLength = 256;
    public const int MaxDescriptionLength = 2048;
}
