using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop_backend.Migrations
{
    /// <inheritdoc />
    public partial class products_add_price_constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_Price",
                table: "Products",
                sql: "\"Price\" > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_Price",
                table: "Products");
        }
    }
}
