using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UMaTLMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UmatId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    NumOfSubClasses = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 801, DateTimeKind.Utc).AddTicks(2441)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 801, DateTimeKind.Utc).AddTicks(5325)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomingCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UmatId = table.Column<int>(type: "int", nullable: true),
                    YearGroup = table.Column<int>(type: "int", nullable: true),
                    AcademicPeriod_LowerYear = table.Column<int>(type: "int", nullable: false),
                    AcademicPeriod_UpperYear = table.Column<int>(type: "int", nullable: false),
                    AcademicPeriod_AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicPeriod_Semester = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    TeachingHours = table.Column<int>(type: "int", nullable: false),
                    PracticalHours = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    CourseGroup = table.Column<int>(type: "int", nullable: false),
                    CourseCategory = table.Column<int>(type: "int", nullable: false),
                    CourseType = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgrammeId = table.Column<int>(type: "int", nullable: true),
                    ProgrammeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstExaminerStaffId = table.Column<int>(type: "int", nullable: true),
                    SecondExaminerStaffId = table.Column<int>(type: "int", nullable: true),
                    IsExaminable = table.Column<bool>(type: "bit", nullable: false),
                    IsToHaveWeeklyLectureSchedule = table.Column<bool>(type: "bit", nullable: false),
                    HasPracticalExams = table.Column<bool>(type: "bit", nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 824, DateTimeKind.Utc).AddTicks(8798)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 825, DateTimeKind.Utc).AddTicks(2324)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UmatId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 895, DateTimeKind.Utc).AddTicks(1922)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 895, DateTimeKind.Utc).AddTicks(7671)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnlineSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimePeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 932, DateTimeKind.Utc).AddTicks(2056)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 932, DateTimeKind.Utc).AddTicks(4179)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOccurred = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateProcessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IsLab = table.Column<bool>(type: "bit", nullable: false),
                    IsIncludedInGeneralAssignment = table.Column<bool>(type: "bit", nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 947, DateTimeKind.Utc).AddTicks(3391)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 947, DateTimeKind.Utc).AddTicks(6013)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubClassGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 965, DateTimeKind.Utc).AddTicks(2911)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 965, DateTimeKind.Utc).AddTicks(5410)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubClassGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubClassGroups_ClassGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ClassGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LecturerId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    PreferredRoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnlineLectureScheduleId = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsPractical = table.Column<bool>(type: "bit", nullable: false),
                    IsVLE = table.Column<bool>(type: "bit", nullable: false),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 867, DateTimeKind.Utc).AddTicks(2835)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 867, DateTimeKind.Utc).AddTicks(5215)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_IncomingCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "IncomingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lectures_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lectures_OnlineSchedules_OnlineLectureScheduleId",
                        column: x => x.OnlineLectureScheduleId,
                        principalTable: "OnlineSchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExamsSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfExam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    ExaminerId = table.Column<int>(type: "int", nullable: true),
                    Examiner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 844, DateTimeKind.Utc).AddTicks(510)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 849, DateTimeKind.Utc).AddTicks(9230)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamsSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamsSchedules_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LectureSubClassGroup",
                columns: table => new
                {
                    LecturesId = table.Column<int>(type: "int", nullable: false),
                    SubClassGroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureSubClassGroup", x => new { x.LecturesId, x.SubClassGroupsId });
                    table.ForeignKey(
                        name: "FK_LectureSubClassGroup_Lectures_LecturesId",
                        column: x => x.LecturesId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureSubClassGroup_SubClassGroups_SubClassGroupsId",
                        column: x => x.SubClassGroupsId,
                        principalTable: "SubClassGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimePeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    FirstLectureId = table.Column<int>(type: "int", nullable: true),
                    SecondLectureId = table.Column<int>(type: "int", nullable: true),
                    Audit_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 918, DateTimeKind.Utc).AddTicks(6050)),
                    Audit_UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValue: new DateTime(2023, 7, 7, 13, 54, 1, 918, DateTimeKind.Utc).AddTicks(8653)),
                    Audit_CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "admin"),
                    Audit_Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Lectures_FirstLectureId",
                        column: x => x.FirstLectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Lectures_SecondLectureId",
                        column: x => x.SecondLectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedules_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamsScheduleLecturer",
                columns: table => new
                {
                    ExamsSchedulesId = table.Column<int>(type: "int", nullable: false),
                    InvigilatorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamsScheduleLecturer", x => new { x.ExamsSchedulesId, x.InvigilatorsId });
                    table.ForeignKey(
                        name: "FK_ExamsScheduleLecturer_ExamsSchedules_ExamsSchedulesId",
                        column: x => x.ExamsSchedulesId,
                        principalTable: "ExamsSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamsScheduleLecturer_Lecturers_InvigilatorsId",
                        column: x => x.InvigilatorsId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamsScheduleSubClassGroup",
                columns: table => new
                {
                    ExamsSchedulesId = table.Column<int>(type: "int", nullable: false),
                    SubClassGroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamsScheduleSubClassGroup", x => new { x.ExamsSchedulesId, x.SubClassGroupsId });
                    table.ForeignKey(
                        name: "FK_ExamsScheduleSubClassGroup_ExamsSchedules_ExamsSchedulesId",
                        column: x => x.ExamsSchedulesId,
                        principalTable: "ExamsSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamsScheduleSubClassGroup_SubClassGroups_SubClassGroupsId",
                        column: x => x.SubClassGroupsId,
                        principalTable: "SubClassGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamsScheduleLecturer_InvigilatorsId",
                table: "ExamsScheduleLecturer",
                column: "InvigilatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamsSchedules_RoomId",
                table: "ExamsSchedules",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamsScheduleSubClassGroup_SubClassGroupsId",
                table: "ExamsScheduleSubClassGroup",
                column: "SubClassGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_CourseId",
                table: "Lectures",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LecturerId",
                table: "Lectures",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_OnlineLectureScheduleId",
                table: "Lectures",
                column: "OnlineLectureScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureSubClassGroup_SubClassGroupsId",
                table: "LectureSubClassGroup",
                column: "SubClassGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FirstLectureId",
                table: "Schedules",
                column: "FirstLectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoomId",
                table: "Schedules",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SecondLectureId",
                table: "Schedules",
                column: "SecondLectureId");

            migrationBuilder.CreateIndex(
                name: "IX_SubClassGroups_GroupId",
                table: "SubClassGroups",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamsScheduleLecturer");

            migrationBuilder.DropTable(
                name: "ExamsScheduleSubClassGroup");

            migrationBuilder.DropTable(
                name: "LectureSubClassGroup");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "ExamsSchedules");

            migrationBuilder.DropTable(
                name: "SubClassGroups");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "ClassGroups");

            migrationBuilder.DropTable(
                name: "IncomingCourses");

            migrationBuilder.DropTable(
                name: "Lecturers");

            migrationBuilder.DropTable(
                name: "OnlineSchedules");
        }
    }
}
