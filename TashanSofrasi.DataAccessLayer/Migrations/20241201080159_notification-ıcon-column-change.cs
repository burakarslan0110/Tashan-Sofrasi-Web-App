using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TashanSofrasi.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class notificationıconcolumnchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotificationIcon",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationIcon",
                table: "Notifications");
        }
    }
}
