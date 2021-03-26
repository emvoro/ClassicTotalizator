using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassicTotalizator.DAL.Migrations
{
    public partial class WalletUpdateV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Accounts_Account_Id",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "Account_Id",
                table: "Wallets",
                newName: "Wallet_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Accounts_Wallet_Id",
                table: "Wallets",
                column: "Wallet_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Accounts_Wallet_Id",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "Wallet_Id",
                table: "Wallets",
                newName: "Account_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Accounts_Account_Id",
                table: "Wallets",
                column: "Account_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
