using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Lecture_PreferredRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Lectures");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 774, DateTimeKind.Utc).AddTicks(54),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 940, DateTimeKind.Utc).AddTicks(2997));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 773, DateTimeKind.Utc).AddTicks(1486),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 940, DateTimeKind.Utc).AddTicks(1770));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 723, DateTimeKind.Utc).AddTicks(4953),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 889, DateTimeKind.Utc).AddTicks(2817));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 723, DateTimeKind.Utc).AddTicks(1631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 889, DateTimeKind.Utc).AddTicks(81));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 754, DateTimeKind.Utc).AddTicks(2935),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 910, DateTimeKind.Utc).AddTicks(6178));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 754, DateTimeKind.Utc).AddTicks(319),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 910, DateTimeKind.Utc).AddTicks(3966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 740, DateTimeKind.Utc).AddTicks(3888),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 899, DateTimeKind.Utc).AddTicks(72));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 740, DateTimeKind.Utc).AddTicks(290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 898, DateTimeKind.Utc).AddTicks(7133));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 677, DateTimeKind.Utc).AddTicks(146),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 836, DateTimeKind.Utc).AddTicks(9225));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 676, DateTimeKind.Utc).AddTicks(7896),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 836, DateTimeKind.Utc).AddTicks(5937));

            migrationBuilder.AddColumn<int>(
                name: "PreferredRoom",
                table: "Lectures",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 707, DateTimeKind.Utc).AddTicks(1381),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 848, DateTimeKind.Utc).AddTicks(4137));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 706, DateTimeKind.Utc).AddTicks(9082),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 848, DateTimeKind.Utc).AddTicks(2316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 640, DateTimeKind.Utc).AddTicks(9409),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 784, DateTimeKind.Utc).AddTicks(6317));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 640, DateTimeKind.Utc).AddTicks(6185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 784, DateTimeKind.Utc).AddTicks(679));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 607, DateTimeKind.Utc).AddTicks(8837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 654, DateTimeKind.Utc).AddTicks(8913));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 607, DateTimeKind.Utc).AddTicks(5584),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 652, DateTimeKind.Utc).AddTicks(1573));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredRoom",
                table: "Lectures");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 940, DateTimeKind.Utc).AddTicks(2997),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 774, DateTimeKind.Utc).AddTicks(54));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 940, DateTimeKind.Utc).AddTicks(1770),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 773, DateTimeKind.Utc).AddTicks(1486));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 889, DateTimeKind.Utc).AddTicks(2817),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 723, DateTimeKind.Utc).AddTicks(4953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 889, DateTimeKind.Utc).AddTicks(81),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 723, DateTimeKind.Utc).AddTicks(1631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 910, DateTimeKind.Utc).AddTicks(6178),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 754, DateTimeKind.Utc).AddTicks(2935));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 910, DateTimeKind.Utc).AddTicks(3966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 754, DateTimeKind.Utc).AddTicks(319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 899, DateTimeKind.Utc).AddTicks(72),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 740, DateTimeKind.Utc).AddTicks(3888));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 898, DateTimeKind.Utc).AddTicks(7133),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 740, DateTimeKind.Utc).AddTicks(290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 836, DateTimeKind.Utc).AddTicks(9225),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 677, DateTimeKind.Utc).AddTicks(146));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 836, DateTimeKind.Utc).AddTicks(5937),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 676, DateTimeKind.Utc).AddTicks(7896));

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 848, DateTimeKind.Utc).AddTicks(4137),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 707, DateTimeKind.Utc).AddTicks(1381));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 848, DateTimeKind.Utc).AddTicks(2316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 706, DateTimeKind.Utc).AddTicks(9082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 784, DateTimeKind.Utc).AddTicks(6317),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 640, DateTimeKind.Utc).AddTicks(9409));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 784, DateTimeKind.Utc).AddTicks(679),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 640, DateTimeKind.Utc).AddTicks(6185));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 654, DateTimeKind.Utc).AddTicks(8913),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 607, DateTimeKind.Utc).AddTicks(8837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 652, DateTimeKind.Utc).AddTicks(1573),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 607, DateTimeKind.Utc).AddTicks(5584));
        }
    }
}
