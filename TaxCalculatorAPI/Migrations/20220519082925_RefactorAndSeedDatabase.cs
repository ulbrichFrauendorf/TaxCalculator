using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculatorAPI.Migrations
{
    public partial class RefactorAndSeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlatRateTaxTable_RateType_RateTypeId",
                table: "FlatRateTaxTable");

            migrationBuilder.DropForeignKey(
                name: "FK_FlatValueTaxTable_RateType_RateTypeId",
                table: "FlatValueTaxTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressiveTaxTable_RateType_RateTypeId",
                table: "ProgressiveTaxTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RateType",
                table: "RateType");

            migrationBuilder.RenameTable(
                name: "RateType",
                newName: "RateTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RateTypes",
                table: "RateTypes",
                column: "RateTypeId");

            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$juRZG2Bz70sZy5vEZ0QVu.2ZCPXwHkJN2Jtu0u1yJUmQnbQaNCgHS");

            migrationBuilder.InsertData(
                table: "RateTypes",
                columns: new[] { "RateTypeId", "RateTypeDescription" },
                values: new object[] { 1, "Amount" });

            migrationBuilder.InsertData(
                table: "RateTypes",
                columns: new[] { "RateTypeId", "RateTypeDescription" },
                values: new object[] { 2, "Percentage" });

            migrationBuilder.InsertData(
                table: "FlatRateTaxTable",
                columns: new[] { "Id", "Active", "RateTypeId", "RateValue" },
                values: new object[] { 1, true, 2, 0.17499999999999999 });

            migrationBuilder.InsertData(
                table: "FlatValueTaxTable",
                columns: new[] { "Id", "Active", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 1, true, 0.0, 2, 0.050000000000000003 });

            migrationBuilder.InsertData(
                table: "FlatValueTaxTable",
                columns: new[] { "Id", "Active", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 2, true, 200000.0, 1, 1000.0 });

            migrationBuilder.InsertData(
                table: "ProgressiveTaxTable",
                columns: new[] { "Id", "Active", "MaximumValue", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 1, true, 8350.0, 0.0, 2, 0.10000000000000001 });

            migrationBuilder.InsertData(
                table: "ProgressiveTaxTable",
                columns: new[] { "Id", "Active", "MaximumValue", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 2, true, 33950.0, 8351.0, 2, 0.14999999999999999 });

            migrationBuilder.InsertData(
                table: "ProgressiveTaxTable",
                columns: new[] { "Id", "Active", "MaximumValue", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 3, true, 82250.0, 33951.0, 2, 0.25 });

            migrationBuilder.InsertData(
                table: "ProgressiveTaxTable",
                columns: new[] { "Id", "Active", "MaximumValue", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 4, true, 171550.0, 82251.0, 2, 0.28000000000000003 });

            migrationBuilder.InsertData(
                table: "ProgressiveTaxTable",
                columns: new[] { "Id", "Active", "MaximumValue", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 5, true, 372950.0, 171551.0, 2, 0.33000000000000002 });

            migrationBuilder.InsertData(
                table: "ProgressiveTaxTable",
                columns: new[] { "Id", "Active", "MaximumValue", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 6, true, 2147483647.0, 372951.0, 2, 0.34999999999999998 });

            migrationBuilder.AddForeignKey(
                name: "FK_FlatRateTaxTable_RateTypes_RateTypeId",
                table: "FlatRateTaxTable",
                column: "RateTypeId",
                principalTable: "RateTypes",
                principalColumn: "RateTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlatValueTaxTable_RateTypes_RateTypeId",
                table: "FlatValueTaxTable",
                column: "RateTypeId",
                principalTable: "RateTypes",
                principalColumn: "RateTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressiveTaxTable_RateTypes_RateTypeId",
                table: "ProgressiveTaxTable",
                column: "RateTypeId",
                principalTable: "RateTypes",
                principalColumn: "RateTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlatRateTaxTable_RateTypes_RateTypeId",
                table: "FlatRateTaxTable");

            migrationBuilder.DropForeignKey(
                name: "FK_FlatValueTaxTable_RateTypes_RateTypeId",
                table: "FlatValueTaxTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressiveTaxTable_RateTypes_RateTypeId",
                table: "ProgressiveTaxTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RateTypes",
                table: "RateTypes");

            migrationBuilder.DeleteData(
                table: "FlatRateTaxTable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FlatValueTaxTable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FlatValueTaxTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProgressiveTaxTable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProgressiveTaxTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProgressiveTaxTable",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProgressiveTaxTable",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProgressiveTaxTable",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProgressiveTaxTable",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RateTypes",
                keyColumn: "RateTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RateTypes",
                keyColumn: "RateTypeId",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "RateTypes",
                newName: "RateType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RateType",
                table: "RateType",
                column: "RateTypeId");

            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$dfcMTXKVC.wiCoP.ufmESeIGOeSa3BtMuQWkCMiSQKz//4yXBMrVa");

            migrationBuilder.AddForeignKey(
                name: "FK_FlatRateTaxTable_RateType_RateTypeId",
                table: "FlatRateTaxTable",
                column: "RateTypeId",
                principalTable: "RateType",
                principalColumn: "RateTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlatValueTaxTable_RateType_RateTypeId",
                table: "FlatValueTaxTable",
                column: "RateTypeId",
                principalTable: "RateType",
                principalColumn: "RateTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressiveTaxTable_RateType_RateTypeId",
                table: "ProgressiveTaxTable",
                column: "RateTypeId",
                principalTable: "RateType",
                principalColumn: "RateTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
