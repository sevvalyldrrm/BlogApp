using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.Migrations
{
    /// <inheritdoc />
    public partial class _1011_check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Post");
        }
    }
}
