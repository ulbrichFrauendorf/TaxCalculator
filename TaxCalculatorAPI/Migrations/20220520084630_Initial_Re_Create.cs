using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculatorAPI.Migrations
{
    public partial class Initial_Re_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiUsers",
                columns: table => new
                {
                    ApiUserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUsers", x => x.ApiUserId);
                });

            migrationBuilder.CreateTable(
                name: "RateTypes",
                columns: table => new
                {
                    RateTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RateTypeDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateTypes", x => x.RateTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TaxType",
                columns: table => new
                {
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxTypeDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxType", x => x.TaxTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FlatRateTaxTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RateValue = table.Column<double>(type: "REAL", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    RateTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatRateTaxTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlatRateTaxTable_RateTypes_RateTypeId",
                        column: x => x.RateTypeId,
                        principalTable: "RateTypes",
                        principalColumn: "RateTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlatValueTaxTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinimumValue = table.Column<double>(type: "REAL", nullable: false),
                    MaximumValue = table.Column<double>(type: "REAL", nullable: false),
                    RateValue = table.Column<double>(type: "REAL", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    RateTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatValueTaxTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlatValueTaxTable_RateTypes_RateTypeId",
                        column: x => x.RateTypeId,
                        principalTable: "RateTypes",
                        principalColumn: "RateTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressiveTaxTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinimumValue = table.Column<double>(type: "REAL", nullable: false),
                    MaximumValue = table.Column<double>(type: "REAL", nullable: false),
                    RateValue = table.Column<double>(type: "REAL", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    RateTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressiveTaxTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgressiveTaxTable_RateTypes_RateTypeId",
                        column: x => x.RateTypeId,
                        principalTable: "RateTypes",
                        principalColumn: "RateTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodeTaxMap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeTaxMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostalCodeTaxMap_TaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "TaxType",
                        principalColumn: "TaxTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxSubmission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnnualIncome = table.Column<double>(type: "REAL", nullable: false),
                    PostalCodeTaxMapId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CalculatedTax = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxSubmission_PostalCodeTaxMap_PostalCodeTaxMapId",
                        column: x => x.PostalCodeTaxMapId,
                        principalTable: "PostalCodeTaxMap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApiUsers",
                columns: new[] { "ApiUserId", "Email", "Password" },
                values: new object[] { 1, "admin@testsite.com", "$2a$11$g2scD.CznFok8NhKilEXi.f/eiBhbkhvryM1pmpdWLFtYbED3ycxq" });

            migrationBuilder.InsertData(
                table: "RateTypes",
                columns: new[] { "RateTypeId", "RateTypeDescription" },
                values: new object[] { 1, "Amount" });

            migrationBuilder.InsertData(
                table: "RateTypes",
                columns: new[] { "RateTypeId", "RateTypeDescription" },
                values: new object[] { 2, "Percentage" });

            migrationBuilder.InsertData(
                table: "TaxType",
                columns: new[] { "TaxTypeId", "TaxTypeDescription" },
                values: new object[] { 1, "Progressive" });

            migrationBuilder.InsertData(
                table: "TaxType",
                columns: new[] { "TaxTypeId", "TaxTypeDescription" },
                values: new object[] { 2, "Flat Value" });

            migrationBuilder.InsertData(
                table: "TaxType",
                columns: new[] { "TaxTypeId", "TaxTypeDescription" },
                values: new object[] { 3, "Flat Rate" });

            migrationBuilder.InsertData(
                table: "FlatRateTaxTable",
                columns: new[] { "Id", "Active", "RateTypeId", "RateValue" },
                values: new object[] { 1, true, 2, 0.17499999999999999 });

            migrationBuilder.InsertData(
                table: "FlatValueTaxTable",
                columns: new[] { "Id", "Active", "MaximumValue", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 1, true, 199000.0, 0.0, 2, 0.050000000000000003 });

            migrationBuilder.InsertData(
                table: "FlatValueTaxTable",
                columns: new[] { "Id", "Active", "MaximumValue", "MinimumValue", "RateTypeId", "RateValue" },
                values: new object[] { 2, true, 2147483647.0, 200000.0, 1, 10000.0 });

            migrationBuilder.InsertData(
                table: "PostalCodeTaxMap",
                columns: new[] { "Id", "PostalCode", "TaxTypeId" },
                values: new object[] { 1, "7441", 1 });

            migrationBuilder.InsertData(
                table: "PostalCodeTaxMap",
                columns: new[] { "Id", "PostalCode", "TaxTypeId" },
                values: new object[] { 2, "A100", 2 });

            migrationBuilder.InsertData(
                table: "PostalCodeTaxMap",
                columns: new[] { "Id", "PostalCode", "TaxTypeId" },
                values: new object[] { 3, "7000", 3 });

            migrationBuilder.InsertData(
                table: "PostalCodeTaxMap",
                columns: new[] { "Id", "PostalCode", "TaxTypeId" },
                values: new object[] { 4, "1000", 1 });

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

            migrationBuilder.CreateIndex(
                name: "IX_FlatRateTaxTable_RateTypeId",
                table: "FlatRateTaxTable",
                column: "RateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatValueTaxTable_RateTypeId",
                table: "FlatValueTaxTable",
                column: "RateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodeTaxMap_TaxTypeId",
                table: "PostalCodeTaxMap",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressiveTaxTable_RateTypeId",
                table: "ProgressiveTaxTable",
                column: "RateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxSubmission_PostalCodeTaxMapId",
                table: "TaxSubmission",
                column: "PostalCodeTaxMapId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUsers");

            migrationBuilder.DropTable(
                name: "FlatRateTaxTable");

            migrationBuilder.DropTable(
                name: "FlatValueTaxTable");

            migrationBuilder.DropTable(
                name: "ProgressiveTaxTable");

            migrationBuilder.DropTable(
                name: "TaxSubmission");

            migrationBuilder.DropTable(
                name: "RateTypes");

            migrationBuilder.DropTable(
                name: "PostalCodeTaxMap");

            migrationBuilder.DropTable(
                name: "TaxType");
        }
    }
}
