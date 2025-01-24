using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelMusic.Migrations
{
    /// <inheritdoc />
    public partial class sepettt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sepetler_gitarlar_Gitarid",
                table: "sepetler");

            migrationBuilder.DropIndex(
                name: "IX_sepetler_Gitarid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "Gitarid",
                table: "sepetler");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gitarid",
                table: "sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_sepetler_Gitarid",
                table: "sepetler",
                column: "Gitarid");

            migrationBuilder.AddForeignKey(
                name: "FK_sepetler_gitarlar_Gitarid",
                table: "sepetler",
                column: "Gitarid",
                principalTable: "gitarlar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
