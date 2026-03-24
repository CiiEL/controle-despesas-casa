using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDespesasCasa.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddReportsAndTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_Categories_CategoryId",
                table: "FinancialTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_People_PersonId",
                table: "FinancialTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialTransactions",
                table: "FinancialTransactions");

            migrationBuilder.RenameTable(
                name: "FinancialTransactions",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransactions_PersonId",
                table: "Transactions",
                newName: "IX_Transactions_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransactions_CategoryId",
                table: "Transactions",
                newName: "IX_Transactions_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_People_PersonId",
                table: "Transactions",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_People_PersonId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "FinancialTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PersonId",
                table: "FinancialTransactions",
                newName: "IX_FinancialTransactions_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CategoryId",
                table: "FinancialTransactions",
                newName: "IX_FinancialTransactions_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialTransactions",
                table: "FinancialTransactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_Categories_CategoryId",
                table: "FinancialTransactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_People_PersonId",
                table: "FinancialTransactions",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
