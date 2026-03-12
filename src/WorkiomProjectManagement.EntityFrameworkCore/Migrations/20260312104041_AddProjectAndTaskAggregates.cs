using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkiomProjectManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectAndTaskAggregates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectMembers",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectMembers", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AppProjectMembers_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppProjectMembers_AppProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppProjectTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AssignedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectTasks_AppProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "AppProjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppProjectTaskAssignmentHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    OldAssignedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    NewAssignedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectTaskAssignmentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectTaskAssignmentHistory_AppProjectTasks_ProjectTask~",
                        column: x => x.ProjectTaskId,
                        principalTable: "AppProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectTaskStatusHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    OldStatus = table.Column<int>(type: "integer", nullable: false),
                    NewStatus = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectTaskStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectTaskStatusHistory_AppProjectTasks_ProjectTaskId",
                        column: x => x.ProjectTaskId,
                        principalTable: "AppProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectMembers_UserId",
                table: "AppProjectMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjects_Name",
                table: "AppProjects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectTaskAssignmentHistory_ProjectTaskId",
                table: "AppProjectTaskAssignmentHistory",
                column: "ProjectTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectTasks_AssignedUserId",
                table: "AppProjectTasks",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectTasks_ProjectId",
                table: "AppProjectTasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectTasks_Status",
                table: "AppProjectTasks",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectTasks_Title",
                table: "AppProjectTasks",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectTaskStatusHistory_ProjectTaskId",
                table: "AppProjectTaskStatusHistory",
                column: "ProjectTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProjectMembers");

            migrationBuilder.DropTable(
                name: "AppProjectTaskAssignmentHistory");

            migrationBuilder.DropTable(
                name: "AppProjectTaskStatusHistory");

            migrationBuilder.DropTable(
                name: "AppProjectTasks");

            migrationBuilder.DropTable(
                name: "AppProjects");
        }
    }
}
