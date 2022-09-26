using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoAranda.Infrastructure.Migrations
{
    public partial class AddedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2527f74e-220a-44d0-af3c-13b6b7584b28", 0, "c22114e3-d4d5-40db-a6a3-2b396c243f36", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEGAjygLz4HfTDjJNyg2zoRZklSMd5fSAQTh9VCnf/Hb1F4uZv6AIXCugvwrAqxgPMw==", null, false, "f5a9873e-aa0a-41a9-bcb8-c2a2cf7ad091", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2527f74e-220a-44d0-af3c-13b6b7584b28");
        }
    }
}
