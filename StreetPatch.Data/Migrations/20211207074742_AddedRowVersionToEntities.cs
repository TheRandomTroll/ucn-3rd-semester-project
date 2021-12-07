using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetPatch.Data.Migrations
{
    public partial class AddedRowVersionToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Reports",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Comments",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Comments");
        }
    }
}
