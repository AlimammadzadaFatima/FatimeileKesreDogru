using Microsoft.EntityFrameworkCore.Migrations;

namespace envoBack.Migrations
{
    public partial class IsDeletedCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelted",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
