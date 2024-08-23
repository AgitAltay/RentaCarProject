using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rac.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReserveToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Reservations");
        }
    }
}
