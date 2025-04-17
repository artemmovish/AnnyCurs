using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminApp.Migrations
{
    /// <inheritdoc />
    public partial class Sec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionImage_Attractions_AttractionId",
                table: "AttractionImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttractionImage",
                table: "AttractionImage");

            migrationBuilder.RenameTable(
                name: "AttractionImage",
                newName: "AttractionImages");

            migrationBuilder.RenameIndex(
                name: "IX_AttractionImage_AttractionId",
                table: "AttractionImages",
                newName: "IX_AttractionImages_AttractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttractionImages",
                table: "AttractionImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionImages_Attractions_AttractionId",
                table: "AttractionImages",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionImages_Attractions_AttractionId",
                table: "AttractionImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttractionImages",
                table: "AttractionImages");

            migrationBuilder.RenameTable(
                name: "AttractionImages",
                newName: "AttractionImage");

            migrationBuilder.RenameIndex(
                name: "IX_AttractionImages_AttractionId",
                table: "AttractionImage",
                newName: "IX_AttractionImage_AttractionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttractionImage",
                table: "AttractionImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionImage_Attractions_AttractionId",
                table: "AttractionImage",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
