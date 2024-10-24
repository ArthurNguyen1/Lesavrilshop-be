using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lesavrilshop_be.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_product_Categories_ParentCategoryId",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_Categories_CategoryId",
                table: "product_category");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_product_ProductId",
                table: "product_category");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_product_ReviewedProductId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_reviews_shop_user_UserId",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_wishlists_product_ProductId",
                table: "wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_wishlists_shop_user_UserId",
                table: "wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_wishlists",
                table: "wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_category",
                table: "product_category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "wishlists",
                newName: "wishlist");

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "review");

            migrationBuilder.RenameTable(
                name: "product_category",
                newName: "product_categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "category");

            migrationBuilder.RenameIndex(
                name: "IX_wishlists_ProductId",
                table: "wishlist",
                newName: "IX_wishlist_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_UserId_ReviewedProductId",
                table: "review",
                newName: "IX_review_UserId_ReviewedProductId");

            migrationBuilder.RenameIndex(
                name: "IX_reviews_ReviewedProductId",
                table: "review",
                newName: "IX_review_ReviewedProductId");

            migrationBuilder.RenameIndex(
                name: "IX_product_category_CategoryId",
                table: "product_categories",
                newName: "IX_product_categories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "category",
                newName: "IX_category_ParentCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "category",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "category",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_wishlist",
                table: "wishlist",
                columns: new[] { "UserId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_review",
                table: "review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_categories",
                table: "product_categories",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                table: "category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_category_category_ParentCategoryId",
                table: "category",
                column: "ParentCategoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_ParentCategoryId",
                table: "product",
                column: "ParentCategoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_review_product_ReviewedProductId",
                table: "review",
                column: "ReviewedProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_review_shop_user_UserId",
                table: "review",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishlist_product_ProductId",
                table: "wishlist",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishlist_shop_user_UserId",
                table: "wishlist",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_category_ParentCategoryId",
                table: "category");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_ParentCategoryId",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_category_CategoryId",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_product_ProductId",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_review_product_ReviewedProductId",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK_review_shop_user_UserId",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK_wishlist_product_ProductId",
                table: "wishlist");

            migrationBuilder.DropForeignKey(
                name: "FK_wishlist_shop_user_UserId",
                table: "wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_wishlist",
                table: "wishlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_review",
                table: "review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_categories",
                table: "product_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                table: "category");

            migrationBuilder.RenameTable(
                name: "wishlist",
                newName: "wishlists");

            migrationBuilder.RenameTable(
                name: "review",
                newName: "reviews");

            migrationBuilder.RenameTable(
                name: "product_categories",
                newName: "product_category");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_wishlist_ProductId",
                table: "wishlists",
                newName: "IX_wishlists_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_review_UserId_ReviewedProductId",
                table: "reviews",
                newName: "IX_reviews_UserId_ReviewedProductId");

            migrationBuilder.RenameIndex(
                name: "IX_review_ReviewedProductId",
                table: "reviews",
                newName: "IX_reviews_ReviewedProductId");

            migrationBuilder.RenameIndex(
                name: "IX_product_categories_CategoryId",
                table: "product_category",
                newName: "IX_product_category_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_category_ParentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_wishlists",
                table: "wishlists",
                columns: new[] { "UserId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_category",
                table: "product_category",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_Categories_ParentCategoryId",
                table: "product",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_Categories_CategoryId",
                table: "product_category",
                column: "CategoryId",
                principalTable: "Categories",
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
                name: "FK_reviews_product_ReviewedProductId",
                table: "reviews",
                column: "ReviewedProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_shop_user_UserId",
                table: "reviews",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishlists_product_ProductId",
                table: "wishlists",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_wishlists_shop_user_UserId",
                table: "wishlists",
                column: "UserId",
                principalTable: "shop_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
