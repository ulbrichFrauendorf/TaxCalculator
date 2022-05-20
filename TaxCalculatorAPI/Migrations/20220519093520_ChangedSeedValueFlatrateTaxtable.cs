using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculatorAPI.Migrations
{
    public partial class ChangedSeedValueFlatrateTaxtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$DIaEwbaG5tvMC9NEe3sdkOjRSBwG53BUw5IdA4FzLerYWfc/FK/86");

            migrationBuilder.UpdateData(
                table: "FlatValueTaxTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "RateValue",
                value: 10000.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$qkvSfgNRxcz08y.wWdYLaegLt9WrPB6tTsbVQ94gANkzrGQ3qgKGy");

            migrationBuilder.UpdateData(
                table: "FlatValueTaxTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "RateValue",
                value: 1000.0);
        }
    }
}
