using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoTrack.WebMvc.Migrations
{
    /// <inheritdoc />
    public partial class ProjectDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmissionFactors",
                columns: table => new
                {
                    EmissionFactorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ActivityType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SubType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    SourceReference = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmissionFactors", x => x.EmissionFactorId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    LifestylePreferences = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActivityType = table.Column<int>(type: "INTEGER", nullable: false),
                    CarbonEmission = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplianceType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    UsageTime = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    PowerRating = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    EmissionFactorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Consumption = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    SourceType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ElectricityActivity_EmissionFactorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FoodType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Source = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FoodActivity_EmissionFactorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Mode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Distance = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    FuelType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LocationStart = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    LocationEnd = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    TravelActivity_EmissionFactorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    WasteType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    WasteActivity_EmissionFactorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_EmissionFactors_ElectricityActivity_EmissionFactorId",
                        column: x => x.ElectricityActivity_EmissionFactorId,
                        principalTable: "EmissionFactors",
                        principalColumn: "EmissionFactorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_EmissionFactors_EmissionFactorId",
                        column: x => x.EmissionFactorId,
                        principalTable: "EmissionFactors",
                        principalColumn: "EmissionFactorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_EmissionFactors_FoodActivity_EmissionFactorId",
                        column: x => x.FoodActivity_EmissionFactorId,
                        principalTable: "EmissionFactors",
                        principalColumn: "EmissionFactorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_EmissionFactors_TravelActivity_EmissionFactorId",
                        column: x => x.TravelActivity_EmissionFactorId,
                        principalTable: "EmissionFactors",
                        principalColumn: "EmissionFactorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_EmissionFactors_WasteActivity_EmissionFactorId",
                        column: x => x.WasteActivity_EmissionFactorId,
                        principalTable: "EmissionFactors",
                        principalColumn: "EmissionFactorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    BadgeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CriteriaMet = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IconUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DateEarned = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.BadgeId);
                    table.ForeignKey(
                        name: "FK_Badges_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaderboardEntries",
                columns: table => new
                {
                    LeaderboardEntryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    Period = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CarbonEmission = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CommunityId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardEntries", x => x.LeaderboardEntryId);
                    table.ForeignKey(
                        name: "FK_LeaderboardEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    SuggestionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    SavingAmount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateTimeIssued = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.SuggestionId);
                    table.ForeignKey(
                        name: "FK_Suggestions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmissionFactors",
                columns: new[] { "EmissionFactorId", "ActivityType", "Region", "SourceReference", "SubType", "Value" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Travel", null, "System", "Cycle", 0m },
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

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ElectricityActivity_EmissionFactorId",
                table: "Activities",
                column: "ElectricityActivity_EmissionFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_EmissionFactorId",
                table: "Activities",
                column: "EmissionFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_FoodActivity_EmissionFactorId",
                table: "Activities",
                column: "FoodActivity_EmissionFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TravelActivity_EmissionFactorId",
                table: "Activities",
                column: "TravelActivity_EmissionFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserId",
                table: "Activities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_WasteActivity_EmissionFactorId",
                table: "Activities",
                column: "WasteActivity_EmissionFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_UserId",
                table: "Badges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardEntries_UserId",
                table: "LeaderboardEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_UserId",
                table: "Suggestions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "LeaderboardEntries");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "EmissionFactors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
