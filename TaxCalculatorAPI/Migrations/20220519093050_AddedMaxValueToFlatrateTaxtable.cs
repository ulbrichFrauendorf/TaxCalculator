using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculatorAPI.Migrations
{
    public partial class AddedMaxValueToFlatrateTaxtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MaximumValue",
                table: "FlatValueTaxTable",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$qkvSfgNRxcz08y.wWdYLaegLt9WrPB6tTsbVQ94gANkzrGQ3qgKGy");

            migrationBuilder.UpdateData(
                table: "FlatValueTaxTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "MaximumValue",
                value: 199000.0);

            migrationBuilder.UpdateData(
                table: "FlatValueTaxTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "MaximumValue",
                value: 2147483647.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumValue",
                table: "FlatValueTaxTable");

            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$juRZG2Bz70sZy5vEZ0QVu.2ZCPXwHkJN2Jtu0u1yJUmQnbQaNCgHS");
        }
    }
}
