using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SerializedCourseCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 765, DateTimeKind.Utc).AddTicks(4451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 965, DateTimeKind.Utc).AddTicks(5410));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 765, DateTimeKind.Utc).AddTicks(2762),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 965, DateTimeKind.Utc).AddTicks(2911));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 728, DateTimeKind.Utc).AddTicks(754),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 918, DateTimeKind.Utc).AddTicks(8653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 727, DateTimeKind.Utc).AddTicks(9458),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 918, DateTimeKind.Utc).AddTicks(6050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 744, DateTimeKind.Utc).AddTicks(9637),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 947, DateTimeKind.Utc).AddTicks(6013));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 744, DateTimeKind.Utc).AddTicks(7800),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 947, DateTimeKind.Utc).AddTicks(3391));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 736, DateTimeKind.Utc).AddTicks(9336),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 932, DateTimeKind.Utc).AddTicks(4179));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 736, DateTimeKind.Utc).AddTicks(8169),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 932, DateTimeKind.Utc).AddTicks(2056));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 711, DateTimeKind.Utc).AddTicks(7967),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 867, DateTimeKind.Utc).AddTicks(5215));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 711, DateTimeKind.Utc).AddTicks(6670),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 867, DateTimeKind.Utc).AddTicks(2835));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 720, DateTimeKind.Utc).AddTicks(4553),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 895, DateTimeKind.Utc).AddTicks(7671));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 720, DateTimeKind.Utc).AddTicks(3637),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 895, DateTimeKind.Utc).AddTicks(1922));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 671, DateTimeKind.Utc).AddTicks(2288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 825, DateTimeKind.Utc).AddTicks(2324));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 670, DateTimeKind.Utc).AddTicks(5246),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 824, DateTimeKind.Utc).AddTicks(8798));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 702, DateTimeKind.Utc).AddTicks(9471),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 849, DateTimeKind.Utc).AddTicks(9230));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 702, DateTimeKind.Utc).AddTicks(8301),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 844, DateTimeKind.Utc).AddTicks(510));

            migrationBuilder.AddColumn<string>(
                name: "SerializedCourseCodes",
                table: "ExamsSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 642, DateTimeKind.Utc).AddTicks(4412),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 801, DateTimeKind.Utc).AddTicks(5325));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 642, DateTimeKind.Utc).AddTicks(3117),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 801, DateTimeKind.Utc).AddTicks(2441));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerializedCourseCodes",
                table: "ExamsSchedules");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 965, DateTimeKind.Utc).AddTicks(5410),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 765, DateTimeKind.Utc).AddTicks(4451));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 965, DateTimeKind.Utc).AddTicks(2911),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 765, DateTimeKind.Utc).AddTicks(2762));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 918, DateTimeKind.Utc).AddTicks(8653),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 728, DateTimeKind.Utc).AddTicks(754));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 918, DateTimeKind.Utc).AddTicks(6050),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 727, DateTimeKind.Utc).AddTicks(9458));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 947, DateTimeKind.Utc).AddTicks(6013),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 744, DateTimeKind.Utc).AddTicks(9637));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 947, DateTimeKind.Utc).AddTicks(3391),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 744, DateTimeKind.Utc).AddTicks(7800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 932, DateTimeKind.Utc).AddTicks(4179),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 736, DateTimeKind.Utc).AddTicks(9336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 932, DateTimeKind.Utc).AddTicks(2056),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 736, DateTimeKind.Utc).AddTicks(8169));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 867, DateTimeKind.Utc).AddTicks(5215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 711, DateTimeKind.Utc).AddTicks(7967));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 867, DateTimeKind.Utc).AddTicks(2835),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 711, DateTimeKind.Utc).AddTicks(6670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 895, DateTimeKind.Utc).AddTicks(7671),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 720, DateTimeKind.Utc).AddTicks(4553));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 895, DateTimeKind.Utc).AddTicks(1922),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 720, DateTimeKind.Utc).AddTicks(3637));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 825, DateTimeKind.Utc).AddTicks(2324),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 671, DateTimeKind.Utc).AddTicks(2288));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 824, DateTimeKind.Utc).AddTicks(8798),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 670, DateTimeKind.Utc).AddTicks(5246));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 849, DateTimeKind.Utc).AddTicks(9230),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 702, DateTimeKind.Utc).AddTicks(9471));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 844, DateTimeKind.Utc).AddTicks(510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 702, DateTimeKind.Utc).AddTicks(8301));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 801, DateTimeKind.Utc).AddTicks(5325),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 642, DateTimeKind.Utc).AddTicks(4412));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 801, DateTimeKind.Utc).AddTicks(2441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 8, 1, 6, 9, 642, DateTimeKind.Utc).AddTicks(3117));
        }
    }
}
