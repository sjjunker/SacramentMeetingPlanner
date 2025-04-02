using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SacramentMeetingPlanner.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planner",
                columns: table => new
                {
                    PlannerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Conducting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningHymn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Invocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SacramentHymn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntermediateHymn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosingHymn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Benediction = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planner", x => x.PlannerId);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerTopic",
                columns: table => new
                {
                    SpeakerTopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlannerId = table.Column<int>(type: "int", nullable: false),
                    Speaker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerTopic", x => x.SpeakerTopicId);
                    table.ForeignKey(
                        name: "FK_SpeakerTopic_Planner_PlannerId",
                        column: x => x.PlannerId,
                        principalTable: "Planner",
                        principalColumn: "PlannerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerTopic_PlannerId",
                table: "SpeakerTopic",
                column: "PlannerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeakerTopic");

            migrationBuilder.DropTable(
                name: "Planner");
        }
    }
}
