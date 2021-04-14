using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModularMonolith.Modules.Conferences.Core.DAL.EF.Migrations
{
    public partial class Conferences_Module_Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "conferences");

            migrationBuilder.CreateTable(
                name: "Conferences",
                schema: "conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ParticipantsLimit = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hosts",
                schema: "conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conferences",
                schema: "conferences");

            migrationBuilder.DropTable(
                name: "Hosts",
                schema: "conferences");
        }
    }
}
