using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.QualificationId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    ApplicationMode = table.Column<int>(type: "int", nullable: false),
                    ApplicationOrder = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    DaytimeEveningAttendance = table.Column<int>(type: "int", nullable: false),
                    PreviousQualificationId = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<int>(type: "int", nullable: false),
                    MotherQualificationId = table.Column<int>(type: "int", nullable: false),
                    FatherQualificationId = table.Column<int>(type: "int", nullable: false),
                    MotherOccupation = table.Column<int>(type: "int", nullable: false),
                    FatherOccupation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Qualifications_FatherQualificationId",
                        column: x => x.FatherQualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Qualifications_MotherQualificationId",
                        column: x => x.MotherQualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Qualifications_PreviousQualificationId",
                        column: x => x.PreviousQualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EconomicIndicators",
                columns: table => new
                {
                    EconomicIndicatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    UnemploymentRate = table.Column<float>(type: "real", nullable: false),
                    InflationRate = table.Column<float>(type: "real", nullable: false),
                    Gdp = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EconomicIndicators", x => x.EconomicIndicatorId);
                    table.ForeignKey(
                        name: "FK_EconomicIndicators_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentPerformances",
                columns: table => new
                {
                    PerformanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits1StSemCredited = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits1StSemEnrolled = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits1StSemEvaluations = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits1StSemApproved = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits1StSemGrade = table.Column<float>(type: "real", nullable: false),
                    CurricularUnits1StSemWithoutEvaluations = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits2NdSemCredited = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits2NdSemEnrolled = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits2NdSemEvaluations = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits2NdSemApproved = table.Column<int>(type: "int", nullable: false),
                    CurricularUnits2NdSemGrade = table.Column<float>(type: "real", nullable: false),
                    CurricularUnits2NdSemWithoutEvaluations = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPerformances", x => x.PerformanceId);
                    table.ForeignKey(
                        name: "FK_StudentPerformances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseName" },
                values: new object[,]
                {
                    { 9, "Course 9" },
                    { 10, "Course 10" },
                    { 11, "Course 11" },
                    { 12, "Course 12" },
                    { 17, "Course 17" }
                });

            migrationBuilder.InsertData(
                table: "Qualifications",
                columns: new[] { "QualificationId", "QualificationName" },
                values: new object[,]
                {
                    { 1, "Qualification 1" },
                    { 10, "Qualification 10" },
                    { 13, "Qualification 13" },
                    { 14, "Qualification 14" },
                    { 22, "Qualification 22" },
                    { 27, "Qualification 27" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "ApplicationMode", "ApplicationOrder", "CourseId", "DaytimeEveningAttendance", "FatherOccupation", "FatherQualificationId", "MaritalStatus", "MotherOccupation", "MotherQualificationId", "Nationality", "PreviousQualificationId" },
                values: new object[,]
                {
                    { 1, 8, 1, 11, 1, 6, 1, 1, 10, 1, 1, 1 },
                    { 2, 13, 1, 9, 1, 9, 1, 1, 5, 13, 1, 1 },
                    { 3, 8, 1, 11, 1, 9, 27, 1, 8, 22, 1, 1 },
                    { 4, 12, 1, 17, 0, 3, 27, 2, 2, 22, 1, 1 },
                    { 5, 8, 1, 10, 1, 9, 14, 1, 10, 22, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "EconomicIndicators",
                columns: new[] { "EconomicIndicatorId", "Gdp", "InflationRate", "StudentId", "UnemploymentRate" },
                values: new object[,]
                {
                    { 1, -0.92f, 0.3f, 1, 16.2f },
                    { 2, -4.06f, 2.8f, 2, 15.5f }
                });

            migrationBuilder.InsertData(
                table: "StudentPerformances",
                columns: new[] { "PerformanceId", "CurricularUnits1StSemApproved", "CurricularUnits1StSemCredited", "CurricularUnits1StSemEnrolled", "CurricularUnits1StSemEvaluations", "CurricularUnits1StSemGrade", "CurricularUnits1StSemWithoutEvaluations", "CurricularUnits2NdSemApproved", "CurricularUnits2NdSemCredited", "CurricularUnits2NdSemEnrolled", "CurricularUnits2NdSemEvaluations", "CurricularUnits2NdSemGrade", "CurricularUnits2NdSemWithoutEvaluations", "StudentId" },
                values: new object[,]
                {
                    { 1, 5, 0, 6, 5, 13.4f, 0, 4, 0, 6, 4, 12.3f, 0, 1 },
                    { 2, 6, 0, 6, 6, 13.4f, 0, 6, 0, 6, 6, 14.2f, 0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EconomicIndicators_StudentId",
                table: "EconomicIndicators",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformances_StudentId",
                table: "StudentPerformances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseId",
                table: "Students",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FatherQualificationId",
                table: "Students",
                column: "FatherQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_MotherQualificationId",
                table: "Students",
                column: "MotherQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PreviousQualificationId",
                table: "Students",
                column: "PreviousQualificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EconomicIndicators");

            migrationBuilder.DropTable(
                name: "StudentPerformances");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Qualifications");
        }
    }
}
