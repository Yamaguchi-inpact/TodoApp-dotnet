using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnetserver.Migrations
{
    public partial class Modifired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "TodoItems",
                newName: "Modified");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "TodoItems",
                newName: "ReleaseDate");
        }
    }
}
