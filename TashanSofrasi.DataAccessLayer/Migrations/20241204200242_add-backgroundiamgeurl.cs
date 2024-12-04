using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TashanSofrasi.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addbackgroundiamgeurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeatureBackgroundImageURL",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureBackgroundImageURL",
                table: "Features");
        }
    }
}
