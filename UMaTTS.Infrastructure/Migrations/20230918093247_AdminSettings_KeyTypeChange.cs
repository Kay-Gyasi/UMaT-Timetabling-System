using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdminSettings_KeyTypeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 945, DateTimeKind.Utc).AddTicks(4199),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 380, DateTimeKind.Utc).AddTicks(3172));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 945, DateTimeKind.Utc).AddTicks(2706),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 377, DateTimeKind.Utc).AddTicks(7285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 868, DateTimeKind.Utc).AddTicks(3931),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 261, DateTimeKind.Utc).AddTicks(424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 868, DateTimeKind.Utc).AddTicks(2301),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 260, DateTimeKind.Utc).AddTicks(6723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 925, DateTimeKind.Utc).AddTicks(4429),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 354, DateTimeKind.Utc).AddTicks(8477));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 925, DateTimeKind.Utc).AddTicks(2018),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 354, DateTimeKind.Utc).AddTicks(5821));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 910, DateTimeKind.Utc).AddTicks(9901),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 337, DateTimeKind.Utc).AddTicks(6698));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 910, DateTimeKind.Utc).AddTicks(6221),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 337, DateTimeKind.Utc).AddTicks(3627));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 884, DateTimeKind.Utc).AddTicks(614),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 285, DateTimeKind.Utc).AddTicks(8339));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 883, DateTimeKind.Utc).AddTicks(9184),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 285, DateTimeKind.Utc).AddTicks(4680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 789, DateTimeKind.Utc).AddTicks(5529),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 191, DateTimeKind.Utc).AddTicks(5216));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 789, DateTimeKind.Utc).AddTicks(3717),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 191, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 807, DateTimeKind.Utc).AddTicks(3304),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 235, DateTimeKind.Utc).AddTicks(2744));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 807, DateTimeKind.Utc).AddTicks(807),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 235, DateTimeKind.Utc).AddTicks(897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 730, DateTimeKind.Utc).AddTicks(4675),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 131, DateTimeKind.Utc).AddTicks(9295));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 729, DateTimeKind.Utc).AddTicks(7866),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 131, DateTimeKind.Utc).AddTicks(6571));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 765, DateTimeKind.Utc).AddTicks(3332),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 171, DateTimeKind.Utc).AddTicks(4884));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 764, DateTimeKind.Utc).AddTicks(9260),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 171, DateTimeKind.Utc).AddTicks(2904));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 688, DateTimeKind.Utc).AddTicks(3408),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 119, DateTimeKind.Utc).AddTicks(4883));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 688, DateTimeKind.Utc).AddTicks(1403),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 119, DateTimeKind.Utc).AddTicks(3394));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 674, DateTimeKind.Utc).AddTicks(2387),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 110, DateTimeKind.Utc).AddTicks(7024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 674, DateTimeKind.Utc).AddTicks(105),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 110, DateTimeKind.Utc).AddTicks(5657));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "AdminSettings",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 612, DateTimeKind.Utc).AddTicks(4773),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 94, DateTimeKind.Utc).AddTicks(7380));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "AdminSettings",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 612, DateTimeKind.Utc).AddTicks(3365),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 94, DateTimeKind.Utc).AddTicks(4740));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 945, DateTimeKind.Utc).AddTicks(4199));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 377, DateTimeKind.Utc).AddTicks(7285),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 945, DateTimeKind.Utc).AddTicks(2706));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 261, DateTimeKind.Utc).AddTicks(424),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 868, DateTimeKind.Utc).AddTicks(3931));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 260, DateTimeKind.Utc).AddTicks(6723),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 868, DateTimeKind.Utc).AddTicks(2301));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 354, DateTimeKind.Utc).AddTicks(8477),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 925, DateTimeKind.Utc).AddTicks(4429));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 354, DateTimeKind.Utc).AddTicks(5821),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 925, DateTimeKind.Utc).AddTicks(2018));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 337, DateTimeKind.Utc).AddTicks(6698),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 910, DateTimeKind.Utc).AddTicks(9901));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 337, DateTimeKind.Utc).AddTicks(3627),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 910, DateTimeKind.Utc).AddTicks(6221));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 285, DateTimeKind.Utc).AddTicks(8339),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 884, DateTimeKind.Utc).AddTicks(614));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 285, DateTimeKind.Utc).AddTicks(4680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 883, DateTimeKind.Utc).AddTicks(9184));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 191, DateTimeKind.Utc).AddTicks(5216),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 789, DateTimeKind.Utc).AddTicks(5529));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 191, DateTimeKind.Utc).AddTicks(3071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 789, DateTimeKind.Utc).AddTicks(3717));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 235, DateTimeKind.Utc).AddTicks(2744),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 807, DateTimeKind.Utc).AddTicks(3304));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 235, DateTimeKind.Utc).AddTicks(897),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 807, DateTimeKind.Utc).AddTicks(807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 131, DateTimeKind.Utc).AddTicks(9295),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 730, DateTimeKind.Utc).AddTicks(4675));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 131, DateTimeKind.Utc).AddTicks(6571),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 729, DateTimeKind.Utc).AddTicks(7866));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 171, DateTimeKind.Utc).AddTicks(4884),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 765, DateTimeKind.Utc).AddTicks(3332));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 171, DateTimeKind.Utc).AddTicks(2904),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 764, DateTimeKind.Utc).AddTicks(9260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 119, DateTimeKind.Utc).AddTicks(4883),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 688, DateTimeKind.Utc).AddTicks(3408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 119, DateTimeKind.Utc).AddTicks(3394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 688, DateTimeKind.Utc).AddTicks(1403));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 110, DateTimeKind.Utc).AddTicks(7024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 674, DateTimeKind.Utc).AddTicks(2387));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 110, DateTimeKind.Utc).AddTicks(5657),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 674, DateTimeKind.Utc).AddTicks(105));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "AdminSettings",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 94, DateTimeKind.Utc).AddTicks(7380),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 612, DateTimeKind.Utc).AddTicks(4773));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "AdminSettings",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 9, 18, 9, 5, 51, 94, DateTimeKind.Utc).AddTicks(4740),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 9, 18, 9, 32, 45, 612, DateTimeKind.Utc).AddTicks(3365));
        }
    }
}
