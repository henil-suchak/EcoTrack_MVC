using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoTrack.WebMvc.Migrations
{
    /// <inheritdoc />
    public partial class NameOfYourRecentChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmissionFactors",
                columns: new[] { "EmissionFactorId", "ActivityType", "Region", "SourceReference", "SubType", "Value" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Travel", null, "Default", "Car", 0.21m },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Food", null, "Default", "Beef", 27.0m },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Food", null, "Default", "Chicken", 6.9m }
                });
        }
    }
}
