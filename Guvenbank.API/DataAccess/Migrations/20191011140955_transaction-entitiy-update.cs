using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class transactionentitiyupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FromCustomerNo",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToCustomerNo",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "No",
                table: "BankAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BankAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FromCustomerNo",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ToCustomerNo",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "BankAccounts",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
