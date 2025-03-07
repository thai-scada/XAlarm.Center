using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XAlarm.Center.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "global_settings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    general_setting = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_global_settings", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "global_settings");
        }
    }
}
