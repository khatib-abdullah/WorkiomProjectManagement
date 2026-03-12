using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using WorkiomProjectManagement.ProjectManagement;

namespace WorkiomProjectManagement.EntityFrameworkCore.ProjectManagement;
public static class ProjectManagementDbContextModelCreatingExtensions
{
    public static void ConfigureProjectManagement(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Project>(b =>
        {
            b.ToTable(WorkiomProjectManagementConsts.DbTablePrefix + "Projects", WorkiomProjectManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name).IsRequired().HasMaxLength(ProjectConsts.MaxNameLength);
            b.Property(x => x.Description).HasMaxLength(ProjectConsts.MaxDescriptionLength);

            b.HasMany(x => x.Members).WithOne().HasForeignKey(x => x.ProjectId).IsRequired();
            b.Navigation(x => x.Members).HasField("_members").UsePropertyAccessMode(PropertyAccessMode.Field);

            b.HasIndex(x => x.Name).IsUnique();
        });

        builder.Entity<ProjectMember>(b =>
        {
            b.ToTable(WorkiomProjectManagementConsts.DbTablePrefix + "ProjectMembers", WorkiomProjectManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(x => new { x.ProjectId, x.UserId });

            b.HasOne<Project>().WithMany(x => x.Members).HasForeignKey(x => x.ProjectId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<ProjectTask>(b =>
        {
            b.ToTable(WorkiomProjectManagementConsts.DbTablePrefix + "ProjectTasks", WorkiomProjectManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Title).IsRequired().HasMaxLength(ProjectTaskConsts.MaxTitleLength);
            b.Property(x => x.Description).HasMaxLength(ProjectTaskConsts.MaxDescriptionLength);

            b.Property(x => x.Priority).IsRequired();
            b.Property(x => x.Status).IsRequired();
            b.Property(x => x.DueDate);
            b.Property(x => x.AssignedUserId);

            b.HasOne<Project>().WithMany().HasForeignKey(x => x.ProjectId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            b.HasMany(x => x.StatusHistory).WithOne().HasForeignKey(x => x.ProjectTaskId).IsRequired();
            b.Navigation(x => x.StatusHistory).HasField("_statusHistory").UsePropertyAccessMode(PropertyAccessMode.Field);

            b.HasMany(x => x.AssignmentHistory).WithOne().HasForeignKey(x => x.ProjectTaskId).IsRequired();
            b.Navigation(x => x.AssignmentHistory).HasField("_assignmentHistory").UsePropertyAccessMode(PropertyAccessMode.Field);

            b.HasIndex(x => x.Title);
            b.HasIndex(x => x.Status);
            b.HasIndex(x => x.AssignedUserId);
        });

        builder.Entity<ProjectTaskAssignmentHistory>(b =>
        {
            b.ToTable(WorkiomProjectManagementConsts.DbTablePrefix + "ProjectTaskAssignmentHistory", WorkiomProjectManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.OldAssignedUserId);
            b.Property(x => x.NewAssignedUserId);

            b.HasIndex(x => x.ProjectTaskId);
        });

        builder.Entity<ProjectTaskStatusHistory>(b =>
        {
            b.ToTable(WorkiomProjectManagementConsts.DbTablePrefix + "ProjectTaskStatusHistory", WorkiomProjectManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.OldStatus);
            b.Property(x => x.NewStatus);

            b.HasIndex(x => x.ProjectTaskId);
        });
    }
}