using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopupProject.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBenefeciaryActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "Topups");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Beneficiaries");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Topups",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Topups");

            migrationBuilder.AddColumn<int>(
                name: "OptionId",
                table: "Topups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Beneficiaries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
