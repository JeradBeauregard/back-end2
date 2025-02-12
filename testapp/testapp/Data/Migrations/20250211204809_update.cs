using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ItemxTypes_ItemId",
                table: "ItemxTypes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemxTypes_TypeId",
                table: "ItemxTypes",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemxTypes_ItemTypes_TypeId",
                table: "ItemxTypes",
                column: "TypeId",
                principalTable: "ItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemxTypes_Items_ItemId",
                table: "ItemxTypes",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemxTypes_ItemTypes_TypeId",
                table: "ItemxTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemxTypes_Items_ItemId",
                table: "ItemxTypes");

            migrationBuilder.DropIndex(
                name: "IX_ItemxTypes_ItemId",
                table: "ItemxTypes");

            migrationBuilder.DropIndex(
                name: "IX_ItemxTypes_TypeId",
                table: "ItemxTypes");
        }
    }
}
