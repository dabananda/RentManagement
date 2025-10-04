using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Shops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "RentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "RentalAgreements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentRecords");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentalAgreements");
        }
    }
}
