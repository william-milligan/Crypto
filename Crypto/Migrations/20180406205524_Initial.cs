using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Crypto.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcountId = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    CurrencyType = table.Column<string>(nullable: true),
                    CurrrencyWanted = table.Column<string>(nullable: true),
                    TransactionDescription = table.Column<string>(nullable: true),
                    TransactionTitle = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Wallet = table.Column<string>(nullable: true),
                    AmountWanted = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
