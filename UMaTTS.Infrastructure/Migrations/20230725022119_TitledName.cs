using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TitledName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 417, DateTimeKind.Utc).AddTicks(7024));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 241, DateTimeKind.Utc).AddTicks(6031),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 417, DateTimeKind.Utc).AddTicks(6064));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 187, DateTimeKind.Utc).AddTicks(1198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 389, DateTimeKind.Utc).AddTicks(4769));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 186, DateTimeKind.Utc).AddTicks(8702),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 389, DateTimeKind.Utc).AddTicks(3432));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 233, DateTimeKind.Utc).AddTicks(2818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 409, DateTimeKind.Utc).AddTicks(4150));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 233, DateTimeKind.Utc).AddTicks(1735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 409, DateTimeKind.Utc).AddTicks(3124));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 217, DateTimeKind.Utc).AddTicks(6688),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 402, DateTimeKind.Utc).AddTicks(8439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 217, DateTimeKind.Utc).AddTicks(4473),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 402, DateTimeKind.Utc).AddTicks(6834));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 198, DateTimeKind.Utc).AddTicks(9180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 395, DateTimeKind.Utc).AddTicks(4895));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 198, DateTimeKind.Utc).AddTicks(6042),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 395, DateTimeKind.Utc).AddTicks(3959));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 141, DateTimeKind.Utc).AddTicks(6067),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 374, DateTimeKind.Utc).AddTicks(9661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 141, DateTimeKind.Utc).AddTicks(4822),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 374, DateTimeKind.Utc).AddTicks(8247));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 148, DateTimeKind.Utc).AddTicks(9039),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 382, DateTimeKind.Utc).AddTicks(3732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 148, DateTimeKind.Utc).AddTicks(7909),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 382, DateTimeKind.Utc).AddTicks(2798));

            migrationBuilder.AddColumn<string>(
                name: "TitledName",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 120, DateTimeKind.Utc).AddTicks(3778),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 353, DateTimeKind.Utc).AddTicks(8526));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 120, DateTimeKind.Utc).AddTicks(2057),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 353, DateTimeKind.Utc).AddTicks(6357));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 132, DateTimeKind.Utc).AddTicks(4607),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 366, DateTimeKind.Utc).AddTicks(1875));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 132, DateTimeKind.Utc).AddTicks(3406),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 366, DateTimeKind.Utc).AddTicks(682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 100, DateTimeKind.Utc).AddTicks(5705),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 345, DateTimeKind.Utc).AddTicks(4109));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 100, DateTimeKind.Utc).AddTicks(4650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 345, DateTimeKind.Utc).AddTicks(3029));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 89, DateTimeKind.Utc).AddTicks(5626),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 333, DateTimeKind.Utc).AddTicks(6680));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 89, DateTimeKind.Utc).AddTicks(4558),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 333, DateTimeKind.Utc).AddTicks(5718));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitledName",
                table: "Lecturers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 417, DateTimeKind.Utc).AddTicks(7024),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 241, DateTimeKind.Utc).AddTicks(7084));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "SubClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 417, DateTimeKind.Utc).AddTicks(6064),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 241, DateTimeKind.Utc).AddTicks(6031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 389, DateTimeKind.Utc).AddTicks(4769),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 187, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Schedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 389, DateTimeKind.Utc).AddTicks(3432),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 186, DateTimeKind.Utc).AddTicks(8702));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 409, DateTimeKind.Utc).AddTicks(4150),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 233, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 409, DateTimeKind.Utc).AddTicks(3124),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 233, DateTimeKind.Utc).AddTicks(1735));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 402, DateTimeKind.Utc).AddTicks(8439),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 217, DateTimeKind.Utc).AddTicks(6688));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Preferences",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 402, DateTimeKind.Utc).AddTicks(6834),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 217, DateTimeKind.Utc).AddTicks(4473));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 395, DateTimeKind.Utc).AddTicks(4895),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 198, DateTimeKind.Utc).AddTicks(9180));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "OnlineSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 395, DateTimeKind.Utc).AddTicks(3959),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 198, DateTimeKind.Utc).AddTicks(6042));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 374, DateTimeKind.Utc).AddTicks(9661),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 141, DateTimeKind.Utc).AddTicks(6067));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lectures",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 374, DateTimeKind.Utc).AddTicks(8247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 141, DateTimeKind.Utc).AddTicks(4822));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 382, DateTimeKind.Utc).AddTicks(3732),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 148, DateTimeKind.Utc).AddTicks(9039));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Lecturers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 382, DateTimeKind.Utc).AddTicks(2798),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 148, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 353, DateTimeKind.Utc).AddTicks(8526),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 120, DateTimeKind.Utc).AddTicks(3778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "IncomingCourses",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 353, DateTimeKind.Utc).AddTicks(6357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 120, DateTimeKind.Utc).AddTicks(2057));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 366, DateTimeKind.Utc).AddTicks(1875),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 132, DateTimeKind.Utc).AddTicks(4607));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ExamsSchedules",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 366, DateTimeKind.Utc).AddTicks(682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 132, DateTimeKind.Utc).AddTicks(3406));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 345, DateTimeKind.Utc).AddTicks(4109),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 100, DateTimeKind.Utc).AddTicks(5705));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "Constraints",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 345, DateTimeKind.Utc).AddTicks(3029),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 100, DateTimeKind.Utc).AddTicks(4650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_UpdatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 333, DateTimeKind.Utc).AddTicks(6680),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 89, DateTimeKind.Utc).AddTicks(5626));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Audit_CreatedAt",
                table: "ClassGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 7, 22, 5, 48, 27, 333, DateTimeKind.Utc).AddTicks(5718),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 7, 25, 2, 21, 19, 89, DateTimeKind.Utc).AddTicks(4558));
        }
    }
}
