using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop_backend.Migrations
{
    /// <inheritdoc />
    public partial class user_deleted_PasswordCofirm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordConfirm",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordConfirm",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
