using Microsoft.EntityFrameworkCore.Migrations;

namespace Paintings.Migrations
{
    public partial class init40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintingOrder_Order_OrderId",
                table: "PaintingOrder");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "PaintingOrder",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintingOrder_Order_OrderId",
                table: "PaintingOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintingOrder_Order_OrderId",
                table: "PaintingOrder");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "PaintingOrder",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaintingOrder_Order_OrderId",
                table: "PaintingOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
