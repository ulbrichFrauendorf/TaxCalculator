using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculatorAPI.Migrations
{
    public partial class AddedTaxCalulatorDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostalCode",
                columns: table => new
                {
                    PostalCodeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCode", x => x.PostalCodeId);
                });

            migrationBuilder.CreateTable(
                name: "RateType",
                columns: table => new
                {
                    RateTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RateTypeDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateType", x => x.RateTypeId);
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
                name: "TaxSubmission",
                columns: table => new
                {
                    TaxSubmissionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnnualIncome = table.Column<double>(type: "REAL", nullable: false),
                    PostalCodeId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CalculatedTax = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxSubmission", x => x.TaxSubmissionId);
                    table.ForeignKey(
                        name: "FK_TaxSubmission_PostalCode_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCode",
                        principalColumn: "PostalCodeId",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_FlatRateTaxTable_RateType_RateTypeId",
                        column: x => x.RateTypeId,
                        principalTable: "RateType",
                        principalColumn: "RateTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlatValueTaxTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinimumValue = table.Column<double>(type: "REAL", nullable: false),
                    RateValue = table.Column<double>(type: "REAL", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    RateTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatValueTaxTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlatValueTaxTable_RateType_RateTypeId",
                        column: x => x.RateTypeId,
                        principalTable: "RateType",
                        principalColumn: "RateTypeId",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_ProgressiveTaxTable_RateType_RateTypeId",
                        column: x => x.RateTypeId,
                        principalTable: "RateType",
                        principalColumn: "RateTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaxTypeSelector",
                columns: table => new
                {
                    TaxTypeSelectorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostalCodeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTypeSelector", x => x.TaxTypeSelectorId);
                    table.ForeignKey(
                        name: "FK_TaxTypeSelector_PostalCode_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCode",
                        principalColumn: "PostalCodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxTypeSelector_TaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "TaxType",
                        principalColumn: "TaxTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$6N1TqM6rPPHxTnt.SSVNOuYrY.k2vbkUNTc2X21ac/Z3HM8vN8dQy");

            migrationBuilder.CreateIndex(
                name: "IX_FlatRateTaxTable_RateTypeId",
                table: "FlatRateTaxTable",
                column: "RateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatValueTaxTable_RateTypeId",
                table: "FlatValueTaxTable",
                column: "RateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressiveTaxTable_RateTypeId",
                table: "ProgressiveTaxTable",
                column: "RateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxSubmission_PostalCodeId",
                table: "TaxSubmission",
                column: "PostalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxTypeSelector_PostalCodeId",
                table: "TaxTypeSelector",
                column: "PostalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxTypeSelector_TaxTypeId",
                table: "TaxTypeSelector",
                column: "TaxTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatRateTaxTable");

            migrationBuilder.DropTable(
                name: "FlatValueTaxTable");

            migrationBuilder.DropTable(
                name: "ProgressiveTaxTable");

            migrationBuilder.DropTable(
                name: "TaxSubmission");

            migrationBuilder.DropTable(
                name: "TaxTypeSelector");

            migrationBuilder.DropTable(
                name: "RateType");

            migrationBuilder.DropTable(
                name: "PostalCode");

            migrationBuilder.DropTable(
                name: "TaxType");

            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Q9Zs1acAHave4P9ojRGXA.rpbkWuAZGDenboe8fyvKXxQLinn8b5.");
        }
    }
}
