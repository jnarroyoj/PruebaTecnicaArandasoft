using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoAranda.Infrastructure.Migrations
{
    public partial class seededUserAndRole2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cdda7ff4 - 4287- 42f4 - b1f6 - 6d710ae37e1e", "93c22be8 - 1dfc - 40a6 - 988d - 409fb86aa29f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdda7ff4 - 4287- 42f4 - b1f6 - 6d710ae37e1e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93c22be8 - 1dfc - 40a6 - 988d - 409fb86aa29f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cdda7ff4-4287-42f4-b1f6-6d710ae37e1e", "99ea7aa1-13c5-4b93-9c4c-3b54531373d8", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "93c22be8-1dfc-40a6-988d-409fb86aa29f", 0, "d1929505-b048-4dbd-b0a0-5f770042e347", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEJS11hd0d79QWaJhS47yRGb28PluATGaCtPKZhguPasAm1NEPDklh2X4x24wbEPTSA==", null, false, "1054b18c-89b1-48e6-bb5f-e23181656c58", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cdda7ff4-4287-42f4-b1f6-6d710ae37e1e", "93c22be8-1dfc-40a6-988d-409fb86aa29f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cdda7ff4-4287-42f4-b1f6-6d710ae37e1e", "93c22be8-1dfc-40a6-988d-409fb86aa29f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdda7ff4-4287-42f4-b1f6-6d710ae37e1e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93c22be8-1dfc-40a6-988d-409fb86aa29f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cdda7ff4 - 4287- 42f4 - b1f6 - 6d710ae37e1e", "17f2a079-ccaf-4823-8192-a6ec922ca69c", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "93c22be8 - 1dfc - 40a6 - 988d - 409fb86aa29f", 0, "6bd93859-6cc7-4094-b077-4388c437d7b4", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEKn7F5ri/H+dhR4vcArhd+5F0OcDStYAvYI4rPo3d1bqWQjYThYamhPsAuvOM61OZg==", null, false, "b25dc4b5-e635-4b46-8a0e-3cddd41d742e", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cdda7ff4 - 4287- 42f4 - b1f6 - 6d710ae37e1e", "93c22be8 - 1dfc - 40a6 - 988d - 409fb86aa29f" });
        }
    }
}
