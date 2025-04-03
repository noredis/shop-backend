using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop_backend.Migrations
{
    /// <inheritdoc />
    public partial class products_change_image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Products",
                newName: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Products",
                newName: "Image");
        }
    }
}
