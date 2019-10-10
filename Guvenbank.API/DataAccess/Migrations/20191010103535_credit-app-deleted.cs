using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class creditappdeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditApplications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApprovedAmount = table.Column<decimal>(nullable: false),
                    CustomerNo = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsWaitingForApproval = table.Column<bool>(nullable: false),
                    RequestedAmount = table.Column<decimal>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditApplications", x => x.Id);
                });
        }
    }
}
