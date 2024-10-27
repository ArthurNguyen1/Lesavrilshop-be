using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lesavrilshop_be.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_image_product_item_ProductItemId",
                table: "product_image");

            migrationBuilder.DropForeignKey(
                name: "FK_product_item_color_ColorId",
                table: "product_item");

            migrationBuilder.DropForeignKey(
                name: "FK_product_item_size_option_SizeId",
                table: "product_item");

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "product_item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "product_item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "product_item",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ProductItemId",
                table: "product_image",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_product_image_product_item_ProductItemId",
                table: "product_image",
                column: "ProductItemId",
                principalTable: "product_item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_item_color_ColorId",
                table: "product_item",
                column: "ColorId",
                principalTable: "color",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_item_size_option_SizeId",
                table: "product_item",
                column: "SizeId",
                principalTable: "size_option",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_image_product_item_ProductItemId",
                table: "product_image");

            migrationBuilder.DropForeignKey(
                name: "FK_product_item_color_ColorId",
                table: "product_item");

            migrationBuilder.DropForeignKey(
                name: "FK_product_item_size_option_SizeId",
                table: "product_item");

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "product_item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "product_item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "product_item",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductItemId",
                table: "product_image",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_product_image_product_item_ProductItemId",
                table: "product_image",
                column: "ProductItemId",
                principalTable: "product_item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_item_color_ColorId",
                table: "product_item",
                column: "ColorId",
                principalTable: "color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_item_size_option_SizeId",
                table: "product_item",
                column: "SizeId",
                principalTable: "size_option",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
