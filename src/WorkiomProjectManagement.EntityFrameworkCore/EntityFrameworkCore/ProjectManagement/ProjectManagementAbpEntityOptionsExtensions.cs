using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using WorkiomProjectManagement.ProjectManagement;

namespace WorkiomProjectManagement.EntityFrameworkCore.ProjectManagement;

public static class ProjectManagementAbpEntityOptionsExtensions
{
    public static void ConfigureProjectManagement(
        this AbpEntityOptions options)
    {
        options.Entity<Project>(options =>
        {
            options.DefaultWithDetailsFunc =
            query => query.Include(x => x.Members);
        });

        options.Entity<ProjectMember>(options =>
        {
            options.DefaultWithDetailsFunc =
            query => query.Include(x => x.User).ThenInclude(x => x.Roles);
        });

        options.Entity<ProjectTask>(options =>
        {
            options.DefaultWithDetailsFunc =
            query => query
                .Include(x => x.AssignmentHistory)
                .Include(x => x.StatusHistory);
        });
    }
}