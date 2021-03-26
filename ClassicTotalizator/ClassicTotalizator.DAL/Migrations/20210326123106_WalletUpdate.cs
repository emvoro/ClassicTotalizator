using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassicTotalizator.DAL.Migrations
{
    public partial class WalletUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Accounts_User_Id",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_User_Id",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Wallets");

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
                name: "FK_Wallets_Accounts_Account_Id",
                table: "Wallets");

            migrationBuilder.AddColumn<Guid>(
                name: "User_Id",
                table: "Wallets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_User_Id",
                table: "Wallets",
                column: "User_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Accounts_User_Id",
                table: "Wallets",
                column: "User_Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
