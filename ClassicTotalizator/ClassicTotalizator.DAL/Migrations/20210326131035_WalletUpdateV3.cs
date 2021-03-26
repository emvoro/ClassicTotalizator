using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassicTotalizator.DAL.Migrations
{
    public partial class WalletUpdateV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_Account_Id",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Accounts_Wallet_Id",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "Wallet_Id",
                table: "Wallets",
                newName: "Account_Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Accounts_Account_Id",
                table: "Wallets",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_Wallet_Id",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Accounts_Account_Id",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "Account_Id",
                table: "Wallets",
                newName: "Wallet_Id");

            migrationBuilder.RenameColumn(
                name: "Wallet_Id",
                table: "Transactions",
                newName: "Account_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_Wallet_Id",
                table: "Transactions",
                newName: "IX_Transactions_Account_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_Account_Id",
                table: "Transactions",
                column: "Account_Id",
                principalTable: "Wallets",
                principalColumn: "Wallet_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Accounts_Wallet_Id",
                table: "Wallets",
                column: "Wallet_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
