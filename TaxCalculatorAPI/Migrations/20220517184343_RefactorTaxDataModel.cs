using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculatorAPI.Migrations
{
    public partial class RefactorTaxDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxSubmission_PostalCode_PostalCodeId",
                table: "TaxSubmission");

            migrationBuilder.DropTable(
                name: "TaxTypeSelector");

            migrationBuilder.DropTable(
                name: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "PostalCodeId",
                table: "TaxSubmission",
                newName: "PostalCodeTaxMapId");

            migrationBuilder.RenameColumn(
                name: "TaxSubmissionId",
                table: "TaxSubmission",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TaxSubmission_PostalCodeId",
                table: "TaxSubmission",
                newName: "IX_TaxSubmission_PostalCodeTaxMapId");

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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "ApiUsers",
                keyColumn: "ApiUserId",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$dfcMTXKVC.wiCoP.ufmESeIGOeSa3BtMuQWkCMiSQKz//4yXBMrVa");

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

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodeTaxMap_TaxTypeId",
                table: "PostalCodeTaxMap",
                column: "TaxTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxSubmission_PostalCodeTaxMap_PostalCodeTaxMapId",
                table: "TaxSubmission",
                column: "PostalCodeTaxMapId",
                principalTable: "PostalCodeTaxMap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxSubmission_PostalCodeTaxMap_PostalCodeTaxMapId",
                table: "TaxSubmission");

            migrationBuilder.DropTable(
                name: "PostalCodeTaxMap");

            migrationBuilder.DeleteData(
                table: "TaxType",
                keyColumn: "TaxTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaxType",
                keyColumn: "TaxTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaxType",
                keyColumn: "TaxTypeId",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "PostalCodeTaxMapId",
                table: "TaxSubmission",
                newName: "PostalCodeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TaxSubmission",
                newName: "TaxSubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxSubmission_PostalCodeTaxMapId",
                table: "TaxSubmission",
                newName: "IX_TaxSubmission_PostalCodeId");

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
                name: "TaxTypeSelector",
                columns: table => new
                {
                    TaxTypeSelectorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostalCodeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTypeSelector", x => x.TaxTypeSelectorId);
                    table.ForeignKey(
                        name: "FK_TaxTypeSelector_PostalCode_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCode",
                        principalColumn: "PostalCodeId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_TaxTypeSelector_PostalCodeId",
                table: "TaxTypeSelector",
                column: "PostalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxTypeSelector_TaxTypeId",
                table: "TaxTypeSelector",
                column: "TaxTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxSubmission_PostalCode_PostalCodeId",
                table: "TaxSubmission",
                column: "PostalCodeId",
                principalTable: "PostalCode",
                principalColumn: "PostalCodeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
