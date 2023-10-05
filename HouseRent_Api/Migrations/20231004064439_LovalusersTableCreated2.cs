using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent_Api.Migrations
{
    /// <inheritdoc />
    public partial class LovalusersTableCreated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "LocalUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "LocalUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
