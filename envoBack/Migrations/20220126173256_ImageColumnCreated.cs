using Microsoft.EntityFrameworkCore.Migrations;

namespace envoBack.Migrations
{
    public partial class ImageColumnCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Workers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Workers");
        }
    }
}
