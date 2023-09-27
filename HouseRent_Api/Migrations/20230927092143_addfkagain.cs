using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRent_Api.Migrations
{
    /// <inheritdoc />
    public partial class addfkagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "HouseNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HouseNumbers_HouseId",
                table: "HouseNumbers",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseNumbers_Houses_HouseId",
                table: "HouseNumbers",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseNumbers_Houses_HouseId",
                table: "HouseNumbers");

            migrationBuilder.DropIndex(
                name: "IX_HouseNumbers_HouseId",
                table: "HouseNumbers");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "HouseNumbers");
        }
    }
}
