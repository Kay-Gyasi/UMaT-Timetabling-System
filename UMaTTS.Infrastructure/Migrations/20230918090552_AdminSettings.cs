using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdminSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 380, DateTimeKind.Utc).AddTicks(3172),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 54, DateTimeKind.Utc).AddTicks(5094));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 377, DateTimeKind.Utc).AddTicks(7285),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 54, DateTimeKind.Utc).AddTicks(3947));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 261, DateTimeKind.Utc).AddTicks(424),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 997, DateTimeKind.Utc).AddTicks(9206));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 260, DateTimeKind.Utc).AddTicks(6723),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 997, DateTimeKind.Utc).AddTicks(5996));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 354, DateTimeKind.Utc).AddTicks(8477),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 37, DateTimeKind.Utc).AddTicks(6811));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 354, DateTimeKind.Utc).AddTicks(5821),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 37, DateTimeKind.Utc).AddTicks(5104));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 337, DateTimeKind.Utc).AddTicks(6698),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 23, DateTimeKind.Utc).AddTicks(8918));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 337, DateTimeKind.Utc).AddTicks(3627),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 23, DateTimeKind.Utc).AddTicks(6574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 285, DateTimeKind.Utc).AddTicks(8339),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 10, DateTimeKind.Utc).AddTicks(1114));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 285, DateTimeKind.Utc).AddTicks(4680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 9, DateTimeKind.Utc).AddTicks(8943));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 191, DateTimeKind.Utc).AddTicks(5216),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 964, DateTimeKind.Utc).AddTicks(223));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 191, DateTimeKind.Utc).AddTicks(3071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 963, DateTimeKind.Utc).AddTicks(7090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 235, DateTimeKind.Utc).AddTicks(2744),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 981, DateTimeKind.Utc).AddTicks(6008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 235, DateTimeKind.Utc).AddTicks(897),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 981, DateTimeKind.Utc).AddTicks(2220));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 131, DateTimeKind.Utc).AddTicks(9295),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 885, DateTimeKind.Utc).AddTicks(563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 131, DateTimeKind.Utc).AddTicks(6571),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 884, DateTimeKind.Utc).AddTicks(3520));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 171, DateTimeKind.Utc).AddTicks(4884),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 942, DateTimeKind.Utc).AddTicks(2222));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 171, DateTimeKind.Utc).AddTicks(2904),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 941, DateTimeKind.Utc).AddTicks(7593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 119, DateTimeKind.Utc).AddTicks(4883),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 866, DateTimeKind.Utc).AddTicks(5069));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 119, DateTimeKind.Utc).AddTicks(3394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 866, DateTimeKind.Utc).AddTicks(2534));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 110, DateTimeKind.Utc).AddTicks(7024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 846, DateTimeKind.Utc).AddTicks(3121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 110, DateTimeKind.Utc).AddTicks(5657),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 846, DateTimeKind.Utc).AddTicks(1904));

            migrationBuilder.CreateTable(
                name: "AdminSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 94, DateTimeKind.Utc).AddTicks(4740)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 94, DateTimeKind.Utc).AddTicks(7380)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminSettings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 54, DateTimeKind.Utc).AddTicks(5094),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 380, DateTimeKind.Utc).AddTicks(3172));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 54, DateTimeKind.Utc).AddTicks(3947),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 377, DateTimeKind.Utc).AddTicks(7285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 997, DateTimeKind.Utc).AddTicks(9206),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 261, DateTimeKind.Utc).AddTicks(424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 997, DateTimeKind.Utc).AddTicks(5996),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 260, DateTimeKind.Utc).AddTicks(6723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 37, DateTimeKind.Utc).AddTicks(6811),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 354, DateTimeKind.Utc).AddTicks(8477));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 37, DateTimeKind.Utc).AddTicks(5104),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 354, DateTimeKind.Utc).AddTicks(5821));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 23, DateTimeKind.Utc).AddTicks(8918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 337, DateTimeKind.Utc).AddTicks(6698));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 23, DateTimeKind.Utc).AddTicks(6574),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 337, DateTimeKind.Utc).AddTicks(3627));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 10, DateTimeKind.Utc).AddTicks(1114),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 285, DateTimeKind.Utc).AddTicks(8339));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 9, DateTimeKind.Utc).AddTicks(8943),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 285, DateTimeKind.Utc).AddTicks(4680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 964, DateTimeKind.Utc).AddTicks(223),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 191, DateTimeKind.Utc).AddTicks(5216));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 963, DateTimeKind.Utc).AddTicks(7090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 191, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 981, DateTimeKind.Utc).AddTicks(6008),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 235, DateTimeKind.Utc).AddTicks(2744));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 981, DateTimeKind.Utc).AddTicks(2220),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 235, DateTimeKind.Utc).AddTicks(897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 885, DateTimeKind.Utc).AddTicks(563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 131, DateTimeKind.Utc).AddTicks(9295));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 884, DateTimeKind.Utc).AddTicks(3520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 131, DateTimeKind.Utc).AddTicks(6571));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 942, DateTimeKind.Utc).AddTicks(2222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 171, DateTimeKind.Utc).AddTicks(4884));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 941, DateTimeKind.Utc).AddTicks(7593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 171, DateTimeKind.Utc).AddTicks(2904));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 866, DateTimeKind.Utc).AddTicks(5069),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 119, DateTimeKind.Utc).AddTicks(4883));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 866, DateTimeKind.Utc).AddTicks(2534),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 119, DateTimeKind.Utc).AddTicks(3394));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 846, DateTimeKind.Utc).AddTicks(3121),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 110, DateTimeKind.Utc).AddTicks(7024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 846, DateTimeKind.Utc).AddTicks(1904),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 110, DateTimeKind.Utc).AddTicks(5657));
        }
    }
}
