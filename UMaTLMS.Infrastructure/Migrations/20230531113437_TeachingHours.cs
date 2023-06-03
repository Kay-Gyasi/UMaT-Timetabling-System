using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TeachingHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLab",
                table: "Lectures",
                newName: "IsPractical");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 644, DateTimeKind.Utc).AddTicks(9656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 534, DateTimeKind.Utc).AddTicks(3794));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 644, DateTimeKind.Utc).AddTicks(6008),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 534, DateTimeKind.Utc).AddTicks(2446));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 576, DateTimeKind.Utc).AddTicks(5932),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 511, DateTimeKind.Utc).AddTicks(8736));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 576, DateTimeKind.Utc).AddTicks(3237),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 511, DateTimeKind.Utc).AddTicks(5973));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 627, DateTimeKind.Utc).AddTicks(1799),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 524, DateTimeKind.Utc).AddTicks(511));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 627, DateTimeKind.Utc).AddTicks(403),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 523, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 534, DateTimeKind.Utc).AddTicks(4576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 492, DateTimeKind.Utc).AddTicks(1930));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 534, DateTimeKind.Utc).AddTicks(1755),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 491, DateTimeKind.Utc).AddTicks(9772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 557, DateTimeKind.Utc).AddTicks(1578),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 501, DateTimeKind.Utc).AddTicks(9356));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 555, DateTimeKind.Utc).AddTicks(745),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 501, DateTimeKind.Utc).AddTicks(7709));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 483, DateTimeKind.Utc).AddTicks(2843),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 480, DateTimeKind.Utc).AddTicks(7089));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 482, DateTimeKind.Utc).AddTicks(9880),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 480, DateTimeKind.Utc).AddTicks(5035));

            migrationBuilder.AddColumn<int>(
                name: "PracticalHours",
                table: "IncomingCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeachingHours",
                table: "IncomingCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 451, DateTimeKind.Utc).AddTicks(8559),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 469, DateTimeKind.Utc).AddTicks(3760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 451, DateTimeKind.Utc).AddTicks(4186),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 469, DateTimeKind.Utc).AddTicks(2490));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PracticalHours",
                table: "IncomingCourses");

            migrationBuilder.DropColumn(
                name: "TeachingHours",
                table: "IncomingCourses");

            migrationBuilder.RenameColumn(
                name: "IsPractical",
                table: "Lectures",
                newName: "IsLab");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 534, DateTimeKind.Utc).AddTicks(3794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 644, DateTimeKind.Utc).AddTicks(9656));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 534, DateTimeKind.Utc).AddTicks(2446),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 644, DateTimeKind.Utc).AddTicks(6008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 511, DateTimeKind.Utc).AddTicks(8736),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 576, DateTimeKind.Utc).AddTicks(5932));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 511, DateTimeKind.Utc).AddTicks(5973),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 576, DateTimeKind.Utc).AddTicks(3237));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 524, DateTimeKind.Utc).AddTicks(511),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 627, DateTimeKind.Utc).AddTicks(1799));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 523, DateTimeKind.Utc).AddTicks(7909),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 627, DateTimeKind.Utc).AddTicks(403));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 492, DateTimeKind.Utc).AddTicks(1930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 534, DateTimeKind.Utc).AddTicks(4576));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 491, DateTimeKind.Utc).AddTicks(9772),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 534, DateTimeKind.Utc).AddTicks(1755));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 501, DateTimeKind.Utc).AddTicks(9356),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 557, DateTimeKind.Utc).AddTicks(1578));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 501, DateTimeKind.Utc).AddTicks(7709),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 555, DateTimeKind.Utc).AddTicks(745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 480, DateTimeKind.Utc).AddTicks(7089),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 483, DateTimeKind.Utc).AddTicks(2843));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 480, DateTimeKind.Utc).AddTicks(5035),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 482, DateTimeKind.Utc).AddTicks(9880));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 469, DateTimeKind.Utc).AddTicks(3760),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 451, DateTimeKind.Utc).AddTicks(8559));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 5, 29, 21, 3, 5, 469, DateTimeKind.Utc).AddTicks(2490),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 5, 31, 11, 34, 36, 451, DateTimeKind.Utc).AddTicks(4186));
        }
    }
}
