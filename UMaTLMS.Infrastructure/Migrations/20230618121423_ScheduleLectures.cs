using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleLectures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Lectures_LectureId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "LectureId",
                table: "Schedules",
                newName: "SecondLectureId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_LectureId",
                table: "Schedules",
                newName: "IX_Schedules_SecondLectureId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 940, DateTimeKind.Utc).AddTicks(2997),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 706, DateTimeKind.Utc).AddTicks(8182));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 940, DateTimeKind.Utc).AddTicks(1770),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 706, DateTimeKind.Utc).AddTicks(6561));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 889, DateTimeKind.Utc).AddTicks(2817),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 675, DateTimeKind.Utc).AddTicks(1162));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 889, DateTimeKind.Utc).AddTicks(81),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 674, DateTimeKind.Utc).AddTicks(9320));

            migrationBuilder.AddColumn<int>(
                name: "FirstLectureId",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 910, DateTimeKind.Utc).AddTicks(6178),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 694, DateTimeKind.Utc).AddTicks(6628));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 910, DateTimeKind.Utc).AddTicks(3966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 694, DateTimeKind.Utc).AddTicks(4178));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 899, DateTimeKind.Utc).AddTicks(72),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 684, DateTimeKind.Utc).AddTicks(1997));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 898, DateTimeKind.Utc).AddTicks(7133),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 684, DateTimeKind.Utc).AddTicks(523));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 836, DateTimeKind.Utc).AddTicks(9225),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 656, DateTimeKind.Utc).AddTicks(1542));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 836, DateTimeKind.Utc).AddTicks(5937),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 655, DateTimeKind.Utc).AddTicks(9106));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 848, DateTimeKind.Utc).AddTicks(4137),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 665, DateTimeKind.Utc).AddTicks(748));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 848, DateTimeKind.Utc).AddTicks(2316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 664, DateTimeKind.Utc).AddTicks(9384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 784, DateTimeKind.Utc).AddTicks(6317),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 634, DateTimeKind.Utc).AddTicks(386));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 784, DateTimeKind.Utc).AddTicks(679),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 633, DateTimeKind.Utc).AddTicks(6237));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 654, DateTimeKind.Utc).AddTicks(8913),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 581, DateTimeKind.Utc).AddTicks(4095));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 652, DateTimeKind.Utc).AddTicks(1573),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 581, DateTimeKind.Utc).AddTicks(2906));

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FirstLectureId",
                table: "Schedules",
                column: "FirstLectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Lectures_FirstLectureId",
                table: "Schedules",
                column: "FirstLectureId",
                principalTable: "Lectures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Lectures_SecondLectureId",
                table: "Schedules",
                column: "SecondLectureId",
                principalTable: "Lectures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Lectures_FirstLectureId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Lectures_SecondLectureId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_FirstLectureId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "FirstLectureId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "SecondLectureId",
                table: "Schedules",
                newName: "LectureId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_SecondLectureId",
                table: "Schedules",
                newName: "IX_Schedules_LectureId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 706, DateTimeKind.Utc).AddTicks(8182),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 940, DateTimeKind.Utc).AddTicks(2997));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 706, DateTimeKind.Utc).AddTicks(6561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 940, DateTimeKind.Utc).AddTicks(1770));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 675, DateTimeKind.Utc).AddTicks(1162),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 889, DateTimeKind.Utc).AddTicks(2817));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 674, DateTimeKind.Utc).AddTicks(9320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 889, DateTimeKind.Utc).AddTicks(81));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 694, DateTimeKind.Utc).AddTicks(6628),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 910, DateTimeKind.Utc).AddTicks(6178));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 694, DateTimeKind.Utc).AddTicks(4178),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 910, DateTimeKind.Utc).AddTicks(3966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 684, DateTimeKind.Utc).AddTicks(1997),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 899, DateTimeKind.Utc).AddTicks(72));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 684, DateTimeKind.Utc).AddTicks(523),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 898, DateTimeKind.Utc).AddTicks(7133));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 656, DateTimeKind.Utc).AddTicks(1542),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 836, DateTimeKind.Utc).AddTicks(9225));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 655, DateTimeKind.Utc).AddTicks(9106),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 836, DateTimeKind.Utc).AddTicks(5937));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 665, DateTimeKind.Utc).AddTicks(748),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 848, DateTimeKind.Utc).AddTicks(4137));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 664, DateTimeKind.Utc).AddTicks(9384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 848, DateTimeKind.Utc).AddTicks(2316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 634, DateTimeKind.Utc).AddTicks(386),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 784, DateTimeKind.Utc).AddTicks(6317));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 633, DateTimeKind.Utc).AddTicks(6237),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 784, DateTimeKind.Utc).AddTicks(679));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 581, DateTimeKind.Utc).AddTicks(4095),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 654, DateTimeKind.Utc).AddTicks(8913));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 17, 9, 59, 7, 581, DateTimeKind.Utc).AddTicks(2906),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 18, 12, 14, 22, 652, DateTimeKind.Utc).AddTicks(1573));

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Lectures_LectureId",
                table: "Schedules",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id");
        }
    }
}
