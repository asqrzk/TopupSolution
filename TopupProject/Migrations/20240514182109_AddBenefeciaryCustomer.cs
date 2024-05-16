using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopupProject.Migrations
{
    /// <inheritdoc />
    public partial class AddBenefeciaryCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topup_Beneficiaries_BeneficiaryId",
                table: "Topup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topup",
                table: "Topup");

            migrationBuilder.RenameTable(
                name: "Topup",
                newName: "Topups");

            migrationBuilder.RenameIndex(
                name: "IX_Topup_BeneficiaryId",
                table: "Topups",
                newName: "IX_Topups_BeneficiaryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topups",
                table: "Topups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topups_Beneficiaries_BeneficiaryId",
                table: "Topups",
                column: "BeneficiaryId",
                principalTable: "Beneficiaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topups_Beneficiaries_BeneficiaryId",
                table: "Topups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Topups",
                table: "Topups");

            migrationBuilder.RenameTable(
                name: "Topups",
                newName: "Topup");

            migrationBuilder.RenameIndex(
                name: "IX_Topups_BeneficiaryId",
                table: "Topup",
                newName: "IX_Topup_BeneficiaryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Topup",
                table: "Topup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topup_Beneficiaries_BeneficiaryId",
                table: "Topup",
                column: "BeneficiaryId",
                principalTable: "Beneficiaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
