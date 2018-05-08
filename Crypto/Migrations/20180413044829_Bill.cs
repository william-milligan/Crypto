using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Crypto.Migrations
{
    public partial class Bill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {





            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcountReported = table.Column<int>(nullable: false),
                    AcountReporting = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportID);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrencyType = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    WalletName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropColumn(
                name: "AcountID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PicUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Wallet",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TransactionInfo",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "AcountId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
