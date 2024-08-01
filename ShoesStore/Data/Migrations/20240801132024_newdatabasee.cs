using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class newdatabasee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_Products_ProductId",
                table: "cart");

            migrationBuilder.DropIndex(
                name: "IX_cart_ProductId",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "cart");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "cartProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cartProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cartProduct_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cartProduct_cart_CartId",
                        column: x => x.CartId,
                        principalTable: "cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cartProduct_CartId",
                table: "cartProduct",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_cartProduct_productId",
                table: "cartProduct",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartProduct");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_ProductId",
                table: "cart",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_Products_ProductId",
                table: "cart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
