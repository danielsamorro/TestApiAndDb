using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditProcessor.Infrastructure.Migrations
{
    public partial class AddCreditTypeToCredit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditType",
                table: "Credits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditType",
                table: "Credits");
        }
    }
}
