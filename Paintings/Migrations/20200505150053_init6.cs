using Microsoft.EntityFrameworkCore.Migrations;

namespace Paintings.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Painting",
                table: "Painting");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Painting");

            migrationBuilder.DropColumn(
                name: "MudiumUsed",
                table: "Painting");

            migrationBuilder.AddColumn<int>(
                name: "PaintingId",
                table: "Painting",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "MediumUsed",
                table: "Painting",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Painting",
                table: "Painting",
                column: "PaintingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Painting",
                table: "Painting");

            migrationBuilder.DropColumn(
                name: "PaintingId",
                table: "Painting");

            migrationBuilder.DropColumn(
                name: "MediumUsed",
                table: "Painting");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Painting",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "MudiumUsed",
                table: "Painting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Painting",
                table: "Painting",
                column: "Id");
        }
    }
}
