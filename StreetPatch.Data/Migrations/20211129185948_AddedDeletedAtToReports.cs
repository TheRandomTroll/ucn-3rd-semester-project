using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetPatch.Data.Migrations
{
    public partial class AddedDeletedAtToReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Reports",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Reports");
        }
    }
}
