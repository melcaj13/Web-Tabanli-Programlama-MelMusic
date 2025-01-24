using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelMusic.Migrations
{
    /// <inheritdoc />
    public partial class I : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "kategori",
                table: "ukuleleler");

            migrationBuilder.DropColumn(
                name: "kategori",
                table: "plaklar");

            migrationBuilder.DropColumn(
                name: "kategori",
                table: "piyanolar");

            migrationBuilder.DropColumn(
                name: "kategori",
                table: "mizikalar");

            migrationBuilder.DropColumn(
                name: "kategori",
                table: "kemanlar");

            migrationBuilder.DropColumn(
                name: "kategori",
                table: "kalimbalar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "kategori",
                table: "ukuleleler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "kategori",
                table: "plaklar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "kategori",
                table: "piyanolar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "kategori",
                table: "mizikalar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "kategori",
                table: "kemanlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "kategori",
                table: "kalimbalar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
