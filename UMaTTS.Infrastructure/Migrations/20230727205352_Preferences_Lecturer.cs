using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Preferences_Lecturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 54, DateTimeKind.Utc).AddTicks(5094),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 241, DateTimeKind.Utc).AddTicks(7084));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 54, DateTimeKind.Utc).AddTicks(3947),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 241, DateTimeKind.Utc).AddTicks(6031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 997, DateTimeKind.Utc).AddTicks(9206),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 187, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 997, DateTimeKind.Utc).AddTicks(5996),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 186, DateTimeKind.Utc).AddTicks(8702));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 37, DateTimeKind.Utc).AddTicks(6811),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 233, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 37, DateTimeKind.Utc).AddTicks(5104),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 233, DateTimeKind.Utc).AddTicks(1735));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 23, DateTimeKind.Utc).AddTicks(8918),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 217, DateTimeKind.Utc).AddTicks(6688));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 23, DateTimeKind.Utc).AddTicks(6574),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 217, DateTimeKind.Utc).AddTicks(4473));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 10, DateTimeKind.Utc).AddTicks(1114),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 198, DateTimeKind.Utc).AddTicks(9180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 9, DateTimeKind.Utc).AddTicks(8943),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 198, DateTimeKind.Utc).AddTicks(6042));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 964, DateTimeKind.Utc).AddTicks(223),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 141, DateTimeKind.Utc).AddTicks(6067));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 963, DateTimeKind.Utc).AddTicks(7090),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 141, DateTimeKind.Utc).AddTicks(4822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 981, DateTimeKind.Utc).AddTicks(6008),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 148, DateTimeKind.Utc).AddTicks(9039));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 981, DateTimeKind.Utc).AddTicks(2220),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 148, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 885, DateTimeKind.Utc).AddTicks(563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 120, DateTimeKind.Utc).AddTicks(3778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 884, DateTimeKind.Utc).AddTicks(3520),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 120, DateTimeKind.Utc).AddTicks(2057));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 942, DateTimeKind.Utc).AddTicks(2222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 132, DateTimeKind.Utc).AddTicks(4607));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 941, DateTimeKind.Utc).AddTicks(7593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 132, DateTimeKind.Utc).AddTicks(3406));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 866, DateTimeKind.Utc).AddTicks(5069),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 100, DateTimeKind.Utc).AddTicks(5705));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 866, DateTimeKind.Utc).AddTicks(2534),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 100, DateTimeKind.Utc).AddTicks(4650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 846, DateTimeKind.Utc).AddTicks(3121),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 89, DateTimeKind.Utc).AddTicks(5626));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 846, DateTimeKind.Utc).AddTicks(1904),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 89, DateTimeKind.Utc).AddTicks(4558));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 241, DateTimeKind.Utc).AddTicks(7084),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 54, DateTimeKind.Utc).AddTicks(5094));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 241, DateTimeKind.Utc).AddTicks(6031),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 54, DateTimeKind.Utc).AddTicks(3947));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 187, DateTimeKind.Utc).AddTicks(1198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 997, DateTimeKind.Utc).AddTicks(9206));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 186, DateTimeKind.Utc).AddTicks(8702),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 997, DateTimeKind.Utc).AddTicks(5996));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 233, DateTimeKind.Utc).AddTicks(2818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 37, DateTimeKind.Utc).AddTicks(6811));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 233, DateTimeKind.Utc).AddTicks(1735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 37, DateTimeKind.Utc).AddTicks(5104));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 217, DateTimeKind.Utc).AddTicks(6688),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 23, DateTimeKind.Utc).AddTicks(8918));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 217, DateTimeKind.Utc).AddTicks(4473),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 23, DateTimeKind.Utc).AddTicks(6574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 198, DateTimeKind.Utc).AddTicks(9180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 10, DateTimeKind.Utc).AddTicks(1114));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 198, DateTimeKind.Utc).AddTicks(6042),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 52, 9, DateTimeKind.Utc).AddTicks(8943));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 141, DateTimeKind.Utc).AddTicks(6067),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 964, DateTimeKind.Utc).AddTicks(223));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 141, DateTimeKind.Utc).AddTicks(4822),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 963, DateTimeKind.Utc).AddTicks(7090));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 148, DateTimeKind.Utc).AddTicks(9039),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 981, DateTimeKind.Utc).AddTicks(6008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 148, DateTimeKind.Utc).AddTicks(7909),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 981, DateTimeKind.Utc).AddTicks(2220));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 120, DateTimeKind.Utc).AddTicks(3778),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 885, DateTimeKind.Utc).AddTicks(563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 120, DateTimeKind.Utc).AddTicks(2057),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 884, DateTimeKind.Utc).AddTicks(3520));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 132, DateTimeKind.Utc).AddTicks(4607),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 942, DateTimeKind.Utc).AddTicks(2222));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 132, DateTimeKind.Utc).AddTicks(3406),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 941, DateTimeKind.Utc).AddTicks(7593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 100, DateTimeKind.Utc).AddTicks(5705),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 866, DateTimeKind.Utc).AddTicks(5069));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 100, DateTimeKind.Utc).AddTicks(4650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 866, DateTimeKind.Utc).AddTicks(2534));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 89, DateTimeKind.Utc).AddTicks(5626),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 846, DateTimeKind.Utc).AddTicks(3121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 89, DateTimeKind.Utc).AddTicks(4558),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 27, 20, 53, 51, 846, DateTimeKind.Utc).AddTicks(1904));
        }
    }
}
