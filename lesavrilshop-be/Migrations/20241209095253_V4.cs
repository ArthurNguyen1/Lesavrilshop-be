using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lesavrilshop_be.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "product_item",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "product_image",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryDescription",
                table: "product",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductItemId1",
                table: "order_item",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductItemId1",
                table: "cart_item",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_item_ProductItemId1",
                table: "order_item",
                column: "ProductItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_cart_item_ProductItemId1",
                table: "cart_item",
                column: "ProductItemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_item_product_item_ProductItemId1",
                table: "cart_item",
                column: "ProductItemId1",
                principalTable: "product_item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_item_product_item_ProductItemId1",
                table: "order_item",
                column: "ProductItemId1",
                principalTable: "product_item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_item_product_item_ProductItemId1",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_item_product_item_ProductItemId1",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "IX_order_item_ProductItemId1",
                table: "order_item");

            migrationBuilder.DropIndex(
                name: "IX_cart_item_ProductItemId1",
                table: "cart_item");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "product_item");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "product_image");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "product");

            migrationBuilder.DropColumn(
                name: "ProductItemId1",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "ProductItemId1",
                table: "cart_item");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryDescription",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
