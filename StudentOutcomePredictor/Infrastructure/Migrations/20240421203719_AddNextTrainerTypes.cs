using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNextTrainerTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TrainerTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "OvaWithSdcaNonCalibrated" },
                    { 3, "LightGbmMulti" },
                    { 4, "NaiveBayes" },
                    { 5, "SdcaMaximumEntropy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TrainerTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TrainerTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TrainerTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TrainerTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
