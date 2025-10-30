using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class managerdeatilIsverified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "verification",
                table: "ManagerDetails",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ManagerDetails",
                keyColumn: "ManagerId",
                keyValue: 1,
                column: "verification",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verification",
                table: "ManagerDetails");
        }
    }
}
