﻿using System;
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
                    line_options = table.Column<string>(type: "jsonb", nullable: false),
                    telegram_options = table.Column<string>(type: "jsonb", nullable: false),
                    email_options = table.Column<string>(type: "jsonb", nullable: false),
                    sms_options = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_global_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    project_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    alarm_payload = table.Column<string>(type: "jsonb", nullable: false),
                    number_of_messages_sent = table.Column<int>(type: "INTEGER", nullable: false),
                    is_success = table.Column<bool>(type: "INTEGER", nullable: false),
                    event_begin_on_utc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    event_end_on_utc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    type = table.Column<int>(type: "INTEGER", nullable: false),
                    type_description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    message_begin = table.Column<string>(type: "TEXT", nullable: false),
                    message_end = table.Column<string>(type: "TEXT", nullable: false),
                    created_by = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    project_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    project_group_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    project_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    valid_until = table.Column<DateTime>(type: "TEXT", nullable: false),
                    invoice_no = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    dongle_id = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    project_options = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_projects_project_id",
                table: "projects",
                column: "project_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "global_settings");

            migrationBuilder.DropTable(
                name: "message_events");

            migrationBuilder.DropTable(
                name: "projects");
        }
    }
}
