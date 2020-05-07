using Microsoft.EntityFrameworkCore.Migrations;

namespace Paintings.Migrations
{
    public partial class init39 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetailViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailViewModel_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLineItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaintingId = table.Column<int>(nullable: true),
                    Cost = table.Column<double>(nullable: false),
                    OrderDetailViewModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLineItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLineItem_OrderDetailViewModel_OrderDetailViewModelId",
                        column: x => x.OrderDetailViewModelId,
                        principalTable: "OrderDetailViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLineItem_Painting_PaintingId",
                        column: x => x.PaintingId,
                        principalTable: "Painting",
                        principalColumn: "PaintingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailViewModel_OrderId",
                table: "OrderDetailViewModel",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineItem_OrderDetailViewModelId",
                table: "OrderLineItem",
                column: "OrderDetailViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLineItem_PaintingId",
                table: "OrderLineItem",
                column: "PaintingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLineItem");

            migrationBuilder.DropTable(
                name: "OrderDetailViewModel");
        }
    }
}
