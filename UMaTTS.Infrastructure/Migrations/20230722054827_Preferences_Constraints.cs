using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Preferences_Constraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 417, DateTimeKind.Utc).AddTicks(7024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 709, DateTimeKind.Utc).AddTicks(7590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 417, DateTimeKind.Utc).AddTicks(6064),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 709, DateTimeKind.Utc).AddTicks(6535));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 389, DateTimeKind.Utc).AddTicks(4769),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 690, DateTimeKind.Utc).AddTicks(845));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 389, DateTimeKind.Utc).AddTicks(3432),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 689, DateTimeKind.Utc).AddTicks(9581));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 409, DateTimeKind.Utc).AddTicks(4150),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 701, DateTimeKind.Utc).AddTicks(7701));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 409, DateTimeKind.Utc).AddTicks(3124),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 701, DateTimeKind.Utc).AddTicks(6706));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 395, DateTimeKind.Utc).AddTicks(4895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 696, DateTimeKind.Utc).AddTicks(2066));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 395, DateTimeKind.Utc).AddTicks(3959),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 696, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 374, DateTimeKind.Utc).AddTicks(9661),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 676, DateTimeKind.Utc).AddTicks(8046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 374, DateTimeKind.Utc).AddTicks(8247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 676, DateTimeKind.Utc).AddTicks(6715));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 382, DateTimeKind.Utc).AddTicks(3732),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 683, DateTimeKind.Utc).AddTicks(3021));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 382, DateTimeKind.Utc).AddTicks(2798),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 683, DateTimeKind.Utc).AddTicks(2120));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 353, DateTimeKind.Utc).AddTicks(8526),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 657, DateTimeKind.Utc).AddTicks(1051));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 353, DateTimeKind.Utc).AddTicks(6357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 656, DateTimeKind.Utc).AddTicks(7728));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 366, DateTimeKind.Utc).AddTicks(1875),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 668, DateTimeKind.Utc).AddTicks(8964));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 366, DateTimeKind.Utc).AddTicks(682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 668, DateTimeKind.Utc).AddTicks(7720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 333, DateTimeKind.Utc).AddTicks(6680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 645, DateTimeKind.Utc).AddTicks(2151));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 333, DateTimeKind.Utc).AddTicks(5718),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 645, DateTimeKind.Utc).AddTicks(1101));

            migrationBuilder.CreateTable(
                name: "Constraints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimetableType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 345, DateTimeKind.Utc).AddTicks(3029)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 345, DateTimeKind.Utc).AddTicks(4109)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constraints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constraints_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimetableType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: true),
                    LectureId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 402, DateTimeKind.Utc).AddTicks(6834)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 402, DateTimeKind.Utc).AddTicks(8439)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preferences_IncomingCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "IncomingCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Preferences_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Preferences_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Constraints_LecturerId",
                table: "Constraints",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_CourseId",
                table: "Preferences",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_LectureId",
                table: "Preferences",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_LecturerId",
                table: "Preferences",
                column: "LecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Constraints");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 709, DateTimeKind.Utc).AddTicks(7590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 417, DateTimeKind.Utc).AddTicks(7024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 709, DateTimeKind.Utc).AddTicks(6535),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 417, DateTimeKind.Utc).AddTicks(6064));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 690, DateTimeKind.Utc).AddTicks(845),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 389, DateTimeKind.Utc).AddTicks(4769));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 689, DateTimeKind.Utc).AddTicks(9581),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 389, DateTimeKind.Utc).AddTicks(3432));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 701, DateTimeKind.Utc).AddTicks(7701),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 409, DateTimeKind.Utc).AddTicks(4150));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 701, DateTimeKind.Utc).AddTicks(6706),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 409, DateTimeKind.Utc).AddTicks(3124));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 696, DateTimeKind.Utc).AddTicks(2066),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 395, DateTimeKind.Utc).AddTicks(4895));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 696, DateTimeKind.Utc).AddTicks(1160),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 395, DateTimeKind.Utc).AddTicks(3959));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 676, DateTimeKind.Utc).AddTicks(8046),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 374, DateTimeKind.Utc).AddTicks(9661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 676, DateTimeKind.Utc).AddTicks(6715),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 374, DateTimeKind.Utc).AddTicks(8247));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 683, DateTimeKind.Utc).AddTicks(3021),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 382, DateTimeKind.Utc).AddTicks(3732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 683, DateTimeKind.Utc).AddTicks(2120),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 382, DateTimeKind.Utc).AddTicks(2798));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 657, DateTimeKind.Utc).AddTicks(1051),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 353, DateTimeKind.Utc).AddTicks(8526));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 656, DateTimeKind.Utc).AddTicks(7728),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 353, DateTimeKind.Utc).AddTicks(6357));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 668, DateTimeKind.Utc).AddTicks(8964),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 366, DateTimeKind.Utc).AddTicks(1875));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 668, DateTimeKind.Utc).AddTicks(7720),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 366, DateTimeKind.Utc).AddTicks(682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 645, DateTimeKind.Utc).AddTicks(2151),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 333, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 10, 7, 35, 10, 645, DateTimeKind.Utc).AddTicks(1101),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 333, DateTimeKind.Utc).AddTicks(5718));
        }
    }
}
