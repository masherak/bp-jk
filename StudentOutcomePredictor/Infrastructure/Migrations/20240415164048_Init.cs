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
                name: "StudyFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyFields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_StudyFields_StudyFieldId",
                        column: x => x.StudyFieldId,
                        principalTable: "StudyFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StudyFields",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Business Management" },
                    { 2, "Softwarový vývoj" }
                });

            migrationBuilder.InsertData(
                table: "Years",
                columns: new[] { "Id", "Year" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name", "StudyFieldId" },
                values: new object[,]
                {
                    { 1, "Anglický jazyk", 1 },
                    { 2, "Finance", 1 },
                    { 3, "Inovace v podnikání", 1 },
                    { 4, "Lidské zdroje", 1 },
                    { 5, "Management", 1 },
                    { 6, "Marketing", 1 },
                    { 7, "Mezinárodní obchod", 1 },
                    { 8, "Podniková etika", 1 },
                    { 9, "Projektový management", 1 },
                    { 10, "Strategické řízení", 1 },
                    { 11, "Účetnictví", 1 },
                    { 12, "Veřejné finance", 1 },
                    { 13, "Algoritmy", 2 },
                    { 14, "Anglický jazyk", 2 },
                    { 15, "Bezpečnost softwaru", 2 },
                    { 16, "Cloud Computing", 2 },
                    { 17, "Databáze", 2 },
                    { 18, "Data Science", 2 },
                    { 19, "Matematika", 2 },
                    { 20, "Obchodní právo", 2 },
                    { 21, "Operační systémy", 2 },
                    { 22, "Programování", 2 },
                    { 23, "Software Engineering", 2 },
                    { 24, "Systémová architektura", 2 },
                    { 25, "UI/UX Design", 2 },
                    { 26, "Umělá inteligence", 2 },
                    { 27, "Web Development", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudyFieldId",
                table: "Subjects",
                column: "StudyFieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropTable(
                name: "StudyFields");
        }
    }
}
