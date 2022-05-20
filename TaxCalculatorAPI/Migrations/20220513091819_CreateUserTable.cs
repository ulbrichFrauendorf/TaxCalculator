using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculatorAPI.Migrations
{
    public partial class CreateUserTable : Migration
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

            migrationBuilder.InsertData(
                table: "ApiUsers",
                columns: new[] { "ApiUserId", "Email", "Password" },
                values: new object[] { 1, "admin@testsite.com", "$2a$11$Q9Zs1acAHave4P9ojRGXA.rpbkWuAZGDenboe8fyvKXxQLinn8b5." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUsers");
        }
    }
}
