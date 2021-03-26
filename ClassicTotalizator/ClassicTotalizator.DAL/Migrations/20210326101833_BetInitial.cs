using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassicTotalizator.DAL.Migrations
{
    public partial class BetInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "BetTime",
                table: "Bets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Bets",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetTime",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bets");
        }
    }
}
