using Microsoft.EntityFrameworkCore.Migrations;

namespace Paintings.Migrations
{
    public partial class ini3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GalleryId",
                table: "Painting",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gallery_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Painting_GalleryId",
                table: "Painting",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_ApplicationUserId",
                table: "Gallery",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Painting_Gallery_GalleryId",
                table: "Painting",
                column: "GalleryId",
                principalTable: "Gallery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Painting_Gallery_GalleryId",
                table: "Painting");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropIndex(
                name: "IX_Painting_GalleryId",
                table: "Painting");

            migrationBuilder.DropColumn(
                name: "GalleryId",
                table: "Painting");
        }
    }
}
