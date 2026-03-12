namespace WorkiomProjectManagement;

public static class WorkiomProjectManagementDomainErrorCodes
{
    public const string ProjectNameAlreadyExists = "WorkiomProjectManagement:Project:00001";

    public const string ProjectMemberAlreadyExists = "WorkiomProjectManagement:ProjectMember:00001";
    public const string ProjectMemberNotExists = "WorkiomProjectManagement:ProjectMember:00002";

    public const string ProjectTaskAssignedUserNotAProjectMember = "WorkiomProjectManagement:ProjectTask:00001";

    public const string ReportEngineUnsupportedReportType = "WorkiomProjectManagement:ReportEngine:00001";
    public const string ReportEngineInvalidReportParameters = "WorkiomProjectManagement:ReportEngine:00002";
}
