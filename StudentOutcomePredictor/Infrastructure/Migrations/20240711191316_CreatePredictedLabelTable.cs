using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatePredictedLabelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PredictedLabelId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PredictedLabels",
                columns: table => new
                {
                    PredictedLabelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictedLabels", x => x.PredictedLabelId);
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "PredictedLabelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "PredictedLabelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3,
                column: "PredictedLabelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 4,
                column: "PredictedLabelId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 5,
                column: "PredictedLabelId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Students_PredictedLabelId",
                table: "Students",
                column: "PredictedLabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_PredictedLabels_PredictedLabelId",
                table: "Students",
                column: "PredictedLabelId",
                principalTable: "PredictedLabels",
                principalColumn: "PredictedLabelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_PredictedLabels_PredictedLabelId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "PredictedLabels");

            migrationBuilder.DropIndex(
                name: "IX_Students_PredictedLabelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PredictedLabelId",
                table: "Students");
        }
    }
}
