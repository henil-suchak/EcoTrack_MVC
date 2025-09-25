using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoTrack.WebMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddEmissionFactorSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_EmissionFactors_ElectricityActivity_EmissionFactorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_EmissionFactors_EmissionFactorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_EmissionFactors_WasteActivity_EmissionFactorId",
                table: "Activities");

            migrationBuilder.InsertData(
                table: "EmissionFactors",
                columns: new[] { "EmissionFactorId", "ActivityType", "Region", "SourceReference", "SubType", "Value" },
                values: new object[,]
                {
                    { new Guid("0a0a0a0a-0000-0000-0000-000000000001"), "Travel", null, "Default", "Car", 0.21m },
                    { new Guid("0a0a0a0a-0000-0000-0000-000000000002"), "Travel", null, "Default", "Bus", 0.10m },
                    { new Guid("0a0a0a0a-0000-0000-0000-000000000003"), "Travel", null, "Default", "Train", 0.04m },
                    { new Guid("0b0b0b0b-0000-0000-0000-000000000001"), "Food", null, "Default", "Beef", 27.0m },
                    { new Guid("0b0b0b0b-0000-0000-0000-000000000002"), "Food", null, "Default", "Chicken", 6.9m },
                    { new Guid("0b0b0b0b-0000-0000-0000-000000000003"), "Food", null, "Default", "Vegetables", 2.0m },
                    { new Guid("0c0c0c0c-0000-0000-0000-000000000001"), "Electricity", null, "Default", "Grid", 0.45m },
                    { new Guid("0d0d0d0d-0000-0000-0000-000000000001"), "Appliance", null, "Default", "Fridge", 0.45m },
                    { new Guid("0d0d0d0d-0000-0000-0000-000000000002"), "Appliance", null, "Default", "AC", 0.45m },
                    { new Guid("0e0e0e0e-0000-0000-0000-000000000001"), "Waste", null, "Default", "Landfill", 0.60m },
                    { new Guid("0e0e0e0e-0000-0000-0000-000000000002"), "Waste", null, "Default", "Recyclable", 0.10m }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_EmissionFactors_ElectricityActivity_EmissionFactorId",
                table: "Activities",
                column: "ElectricityActivity_EmissionFactorId",
                principalTable: "EmissionFactors",
                principalColumn: "EmissionFactorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_EmissionFactors_EmissionFactorId",
                table: "Activities",
                column: "EmissionFactorId",
                principalTable: "EmissionFactors",
                principalColumn: "EmissionFactorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_EmissionFactors_WasteActivity_EmissionFactorId",
                table: "Activities",
                column: "WasteActivity_EmissionFactorId",
                principalTable: "EmissionFactors",
                principalColumn: "EmissionFactorId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_EmissionFactors_ElectricityActivity_EmissionFactorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_EmissionFactors_EmissionFactorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_EmissionFactors_WasteActivity_EmissionFactorId",
                table: "Activities");

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0a0a0a0a-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0a0a0a0a-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0a0a0a0a-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0b0b0b0b-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0b0b0b0b-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0b0b0b0b-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0c0c0c0c-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0d0d0d0d-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0d0d0d0d-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0e0e0e0e-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "EmissionFactors",
                keyColumn: "EmissionFactorId",
                keyValue: new Guid("0e0e0e0e-0000-0000-0000-000000000002"));

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_EmissionFactors_ElectricityActivity_EmissionFactorId",
                table: "Activities",
                column: "ElectricityActivity_EmissionFactorId",
                principalTable: "EmissionFactors",
                principalColumn: "EmissionFactorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_EmissionFactors_EmissionFactorId",
                table: "Activities",
                column: "EmissionFactorId",
                principalTable: "EmissionFactors",
                principalColumn: "EmissionFactorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_EmissionFactors_WasteActivity_EmissionFactorId",
                table: "Activities",
                column: "WasteActivity_EmissionFactorId",
                principalTable: "EmissionFactors",
                principalColumn: "EmissionFactorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
