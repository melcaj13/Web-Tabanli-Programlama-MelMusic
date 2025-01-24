using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelMusic.Migrations
{
    /// <inheritdoc />
    public partial class S : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gitarid",
                table: "sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kalimbaid",
                table: "sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kemanid",
                table: "sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mizikaid",
                table: "sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Piyanoid",
                table: "sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Plakid",
                table: "sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Ukuleleid",
                table: "sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrunTuru",
                table: "sepetler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_sepetler_Gitarid",
                table: "sepetler",
                column: "Gitarid");

            migrationBuilder.CreateIndex(
                name: "IX_sepetler_Kalimbaid",
                table: "sepetler",
                column: "Kalimbaid");

            migrationBuilder.CreateIndex(
                name: "IX_sepetler_Kemanid",
                table: "sepetler",
                column: "Kemanid");

            migrationBuilder.CreateIndex(
                name: "IX_sepetler_Mizikaid",
                table: "sepetler",
                column: "Mizikaid");

            migrationBuilder.CreateIndex(
                name: "IX_sepetler_Piyanoid",
                table: "sepetler",
                column: "Piyanoid");

            migrationBuilder.CreateIndex(
                name: "IX_sepetler_Plakid",
                table: "sepetler",
                column: "Plakid");

            migrationBuilder.CreateIndex(
                name: "IX_sepetler_Ukuleleid",
                table: "sepetler",
                column: "Ukuleleid");

            migrationBuilder.AddForeignKey(
                name: "FK_sepetler_gitarlar_Gitarid",
                table: "sepetler",
                column: "Gitarid",
                principalTable: "gitarlar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sepetler_kalimbalar_Kalimbaid",
                table: "sepetler",
                column: "Kalimbaid",
                principalTable: "kalimbalar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sepetler_kemanlar_Kemanid",
                table: "sepetler",
                column: "Kemanid",
                principalTable: "kemanlar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sepetler_mizikalar_Mizikaid",
                table: "sepetler",
                column: "Mizikaid",
                principalTable: "mizikalar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sepetler_piyanolar_Piyanoid",
                table: "sepetler",
                column: "Piyanoid",
                principalTable: "piyanolar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sepetler_plaklar_Plakid",
                table: "sepetler",
                column: "Plakid",
                principalTable: "plaklar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sepetler_ukuleleler_Ukuleleid",
                table: "sepetler",
                column: "Ukuleleid",
                principalTable: "ukuleleler",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sepetler_gitarlar_Gitarid",
                table: "sepetler");

            migrationBuilder.DropForeignKey(
                name: "FK_sepetler_kalimbalar_Kalimbaid",
                table: "sepetler");

            migrationBuilder.DropForeignKey(
                name: "FK_sepetler_kemanlar_Kemanid",
                table: "sepetler");

            migrationBuilder.DropForeignKey(
                name: "FK_sepetler_mizikalar_Mizikaid",
                table: "sepetler");

            migrationBuilder.DropForeignKey(
                name: "FK_sepetler_piyanolar_Piyanoid",
                table: "sepetler");

            migrationBuilder.DropForeignKey(
                name: "FK_sepetler_plaklar_Plakid",
                table: "sepetler");

            migrationBuilder.DropForeignKey(
                name: "FK_sepetler_ukuleleler_Ukuleleid",
                table: "sepetler");

            migrationBuilder.DropIndex(
                name: "IX_sepetler_Gitarid",
                table: "sepetler");

            migrationBuilder.DropIndex(
                name: "IX_sepetler_Kalimbaid",
                table: "sepetler");

            migrationBuilder.DropIndex(
                name: "IX_sepetler_Kemanid",
                table: "sepetler");

            migrationBuilder.DropIndex(
                name: "IX_sepetler_Mizikaid",
                table: "sepetler");

            migrationBuilder.DropIndex(
                name: "IX_sepetler_Piyanoid",
                table: "sepetler");

            migrationBuilder.DropIndex(
                name: "IX_sepetler_Plakid",
                table: "sepetler");

            migrationBuilder.DropIndex(
                name: "IX_sepetler_Ukuleleid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "Gitarid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "Kalimbaid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "Kemanid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "Mizikaid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "Piyanoid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "Plakid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "Ukuleleid",
                table: "sepetler");

            migrationBuilder.DropColumn(
                name: "UrunTuru",
                table: "sepetler");
        }
    }
}
