using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperNoteApp.Migrations
{
    public partial class NoteUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Notes",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Notes");
        }
    }
}
