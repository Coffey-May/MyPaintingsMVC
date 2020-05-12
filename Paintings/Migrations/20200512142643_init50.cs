using Microsoft.EntityFrameworkCore.Migrations;

namespace Paintings.Migrations
{
    public partial class init50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaintingId",
                table: "Gallery",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaintingId",
                table: "Gallery");
        }
    }
}
