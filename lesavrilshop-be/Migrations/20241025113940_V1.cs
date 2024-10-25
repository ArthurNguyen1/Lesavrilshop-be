using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lesavrilshop_be.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_category_ParentCategoryId",
                table: "product");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "product",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_ParentCategoryId",
                table: "product",
                column: "ParentCategoryId",
                principalTable: "category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_category_ParentCategoryId",
                table: "product");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCategoryId",
                table: "product",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_ParentCategoryId",
                table: "product",
                column: "ParentCategoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
