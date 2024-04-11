using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HHPWBE.Migrations
{
    public partial class orderTypeIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderType",
                table: "Orders",
                newName: "OrderTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderTypeId",
                table: "Orders",
                newName: "OrderType");
        }
    }
}
