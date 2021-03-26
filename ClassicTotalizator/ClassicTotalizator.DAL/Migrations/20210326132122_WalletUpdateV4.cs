using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassicTotalizator.DAL.Migrations
{
    public partial class WalletUpdateV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_Account_Id",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Account_Id",
                table: "Transactions",
                newName: "Wallet_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_Account_Id",
                table: "Transactions",
                newName: "IX_Transactions_Wallet_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_Wallet_Id",
                table: "Transactions",
                column: "Wallet_Id",
                principalTable: "Wallets",
                principalColumn: "Account_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_Wallet_Id",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Wallet_Id",
                table: "Transactions",
                newName: "Account_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_Wallet_Id",
                table: "Transactions",
                newName: "IX_Transactions_Account_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_Account_Id",
                table: "Transactions",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
