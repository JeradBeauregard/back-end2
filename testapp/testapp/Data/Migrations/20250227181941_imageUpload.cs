using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class imageUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPic",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PicPath",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPic",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PicPath",
                table: "Items");
        }
    }
}
