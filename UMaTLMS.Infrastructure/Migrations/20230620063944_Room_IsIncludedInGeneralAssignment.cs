using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Room_IsIncludedInGeneralAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 498, DateTimeKind.Utc).AddTicks(811),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 774, DateTimeKind.Utc).AddTicks(54));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 497, DateTimeKind.Utc).AddTicks(8987),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 773, DateTimeKind.Utc).AddTicks(1486));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 440, DateTimeKind.Utc).AddTicks(7828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 723, DateTimeKind.Utc).AddTicks(4953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 435, DateTimeKind.Utc).AddTicks(6828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 723, DateTimeKind.Utc).AddTicks(1631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 463, DateTimeKind.Utc).AddTicks(3326),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 754, DateTimeKind.Utc).AddTicks(2935));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 463, DateTimeKind.Utc).AddTicks(413),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 754, DateTimeKind.Utc).AddTicks(319));

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludedInGeneralAssignment",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 449, DateTimeKind.Utc).AddTicks(9804),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 740, DateTimeKind.Utc).AddTicks(3888));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 449, DateTimeKind.Utc).AddTicks(7798),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 740, DateTimeKind.Utc).AddTicks(290));

            migrationBuilder.AlterColumn<string>(
                name: "PreferredRoom",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 408, DateTimeKind.Utc).AddTicks(9092),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 677, DateTimeKind.Utc).AddTicks(146));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 408, DateTimeKind.Utc).AddTicks(6245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 676, DateTimeKind.Utc).AddTicks(7896));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 418, DateTimeKind.Utc).AddTicks(4142),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 707, DateTimeKind.Utc).AddTicks(1381));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 418, DateTimeKind.Utc).AddTicks(2566),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 706, DateTimeKind.Utc).AddTicks(9082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 364, DateTimeKind.Utc).AddTicks(8917),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 640, DateTimeKind.Utc).AddTicks(9409));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 364, DateTimeKind.Utc).AddTicks(842),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 640, DateTimeKind.Utc).AddTicks(6185));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 327, DateTimeKind.Utc).AddTicks(4202),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 607, DateTimeKind.Utc).AddTicks(8837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 327, DateTimeKind.Utc).AddTicks(383),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 607, DateTimeKind.Utc).AddTicks(5584));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIncludedInGeneralAssignment",
                table: "Rooms");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 774, DateTimeKind.Utc).AddTicks(54),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 498, DateTimeKind.Utc).AddTicks(811));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 773, DateTimeKind.Utc).AddTicks(1486),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 497, DateTimeKind.Utc).AddTicks(8987));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 723, DateTimeKind.Utc).AddTicks(4953),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 440, DateTimeKind.Utc).AddTicks(7828));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 723, DateTimeKind.Utc).AddTicks(1631),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 435, DateTimeKind.Utc).AddTicks(6828));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 754, DateTimeKind.Utc).AddTicks(2935),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 463, DateTimeKind.Utc).AddTicks(3326));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 754, DateTimeKind.Utc).AddTicks(319),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 463, DateTimeKind.Utc).AddTicks(413));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 740, DateTimeKind.Utc).AddTicks(3888),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 449, DateTimeKind.Utc).AddTicks(9804));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 740, DateTimeKind.Utc).AddTicks(290),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 449, DateTimeKind.Utc).AddTicks(7798));

            migrationBuilder.AlterColumn<int>(
                name: "PreferredRoom",
                table: "Lectures",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 677, DateTimeKind.Utc).AddTicks(146),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 408, DateTimeKind.Utc).AddTicks(9092));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 676, DateTimeKind.Utc).AddTicks(7896),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 408, DateTimeKind.Utc).AddTicks(6245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 707, DateTimeKind.Utc).AddTicks(1381),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 418, DateTimeKind.Utc).AddTicks(4142));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 706, DateTimeKind.Utc).AddTicks(9082),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 418, DateTimeKind.Utc).AddTicks(2566));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 640, DateTimeKind.Utc).AddTicks(9409),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 364, DateTimeKind.Utc).AddTicks(8917));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 640, DateTimeKind.Utc).AddTicks(6185),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 364, DateTimeKind.Utc).AddTicks(842));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 607, DateTimeKind.Utc).AddTicks(8837),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 327, DateTimeKind.Utc).AddTicks(4202));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 19, 12, 6, 36, 607, DateTimeKind.Utc).AddTicks(5584),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 20, 6, 39, 43, 327, DateTimeKind.Utc).AddTicks(383));
        }
    }
}
