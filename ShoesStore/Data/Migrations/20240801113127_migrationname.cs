using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoesStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class migrationname : Migration
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

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.CartsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CartProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_cart_CartsId",
                        column: x => x.CartsId,
                        principalTable: "cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductsId",
                table: "CartProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProduct");

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
