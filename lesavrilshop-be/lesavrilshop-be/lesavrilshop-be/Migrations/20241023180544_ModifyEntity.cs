using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace lesavrilshop_be.Migrations
{
    /// <inheritdoc />
    public partial class ModifyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_category_CategoryId",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_product_ProductId",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_product_item_Colors_ColorId",
                table: "product_item");

            migrationBuilder.DropForeignKey(
                name: "FK_product_item_SizeOptions_SizeId",
                table: "product_item");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_product_item_ProductItemId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_address_AddressId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_shop_user_UserId",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SizeOptions",
                table: "SizeOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_categories",
                table: "product_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "user_address");

            migrationBuilder.RenameTable(
                name: "SizeOptions",
                newName: "size_option");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "product_image");

            migrationBuilder.RenameTable(
                name: "product_categories",
                newName: "product_category");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "color");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddresses_AddressId",
                table: "user_address",
                newName: "IX_user_address_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductItemId",
                table: "product_image",
                newName: "IX_product_image_ProductItemId");

            migrationBuilder.RenameIndex(
                name: "IX_product_categories_CategoryId",
                table: "product_category",
                newName: "IX_product_category_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "user_address",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "SizeName",
                table: "size_option",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "product_image",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "color",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_address",
                table: "user_address",
                columns: new[] { "UserId", "AddressId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_size_option",
                table: "size_option",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_image",
                table: "product_image",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_category",
                table: "product_category",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_color",
                table: "color",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_category_CategoryId",
                table: "product_category",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_product_ProductId",
                table: "product_category",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_user_address_address_AddressId",
                table: "user_address",
                column: "AddressId",
                principalTable: "address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_address_shop_user_UserId",
                table: "user_address",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_category_category_CategoryId",
                table: "product_category");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_product_ProductId",
                table: "product_category");

            migrationBuilder.DropForeignKey(
                name: "FK_product_image_product_item_ProductItemId",
                table: "product_image");

            migrationBuilder.DropForeignKey(
                name: "FK_product_item_color_ColorId",
                table: "product_item");

            migrationBuilder.DropForeignKey(
                name: "FK_product_item_size_option_SizeId",
                table: "product_item");

            migrationBuilder.DropForeignKey(
                name: "FK_user_address_address_AddressId",
                table: "user_address");

            migrationBuilder.DropForeignKey(
                name: "FK_user_address_shop_user_UserId",
                table: "user_address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_address",
                table: "user_address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_size_option",
                table: "size_option");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_image",
                table: "product_image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_category",
                table: "product_category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_color",
                table: "color");

            migrationBuilder.RenameTable(
                name: "user_address",
                newName: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "size_option",
                newName: "SizeOptions");

            migrationBuilder.RenameTable(
                name: "product_image",
                newName: "ProductImages");

            migrationBuilder.RenameTable(
                name: "product_category",
                newName: "product_categories");

            migrationBuilder.RenameTable(
                name: "color",
                newName: "Colors");

            migrationBuilder.RenameIndex(
                name: "IX_user_address_AddressId",
                table: "UserAddresses",
                newName: "IX_UserAddresses_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_product_image_ProductItemId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductItemId");

            migrationBuilder.RenameIndex(
                name: "IX_product_category_CategoryId",
                table: "product_categories",
                newName: "IX_product_categories_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserAddresses",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "SizeName",
                table: "SizeOptions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ProductImages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ColorName",
                table: "Colors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SizeOptions",
                table: "SizeOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_categories",
                table: "product_categories",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_categories_category_CategoryId",
                table: "product_categories",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_categories_product_ProductId",
                table: "product_categories",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_item_Colors_ColorId",
                table: "product_item",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_item_SizeOptions_SizeId",
                table: "product_item",
                column: "SizeId",
                principalTable: "SizeOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_product_item_ProductItemId",
                table: "ProductImages",
                column: "ProductItemId",
                principalTable: "product_item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_address_AddressId",
                table: "UserAddresses",
                column: "AddressId",
                principalTable: "address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_shop_user_UserId",
                table: "UserAddresses",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
