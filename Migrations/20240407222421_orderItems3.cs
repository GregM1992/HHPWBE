using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHPWBE.Migrations
{
    public partial class orderItems3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Items_ItemInOrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderContainingItemId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "OrderContainingItemId",
                table: "OrderItem",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "ItemInOrderId",
                table: "OrderItem",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderContainingItemId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ItemInOrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Items_ItemId",
                table: "OrderItem",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Items_ItemId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderItem",
                newName: "OrderContainingItemId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "OrderItem",
                newName: "ItemInOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderContainingItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ItemId",
                table: "OrderItem",
                newName: "IX_OrderItem_ItemInOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Items_ItemInOrderId",
                table: "OrderItem",
                column: "ItemInOrderId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderContainingItemId",
                table: "OrderItem",
                column: "OrderContainingItemId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
