using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoAranda.Infrastructure.Migrations
{
    public partial class ChangedDescriptionTypeToNvarchar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_Nombre",
                table: "Productos");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d05ad604-2a69-46ff-98d2-f2d1c134bbea");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Productos",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Productos",
                type: "nvarchar(1000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "93c22be8-1dfc-40a6-988d-409fb86aa29f", 0, "31c2be8a-e2a0-46f4-9784-b0f9adfcd120", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEI41OlMMkoD/y+OedBspTtCeXnP3qPi9S1HzUmDZpAErRf0S7SIsczTuZ2CEI46eKw==", null, false, "814b9d06-ae78-44b9-835f-30868545cb93", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Nombre",
                table: "Productos",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_Nombre",
                table: "Productos");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93c22be8-1dfc-40a6-988d-409fb86aa29f");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Productos",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Productos",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d05ad604-2a69-46ff-98d2-f2d1c134bbea", 0, "50737add-9d33-4f12-b514-b16968e010d3", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEN5ciByIlFYhHeoiK9Ss3rpGf5mehsZv0iqskQZlrc5aw4ozxA8fzxMgcRVx6rzQCQ==", null, false, "d352ac01-faf3-47a3-913c-5f2319856a33", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Nombre",
                table: "Productos",
                column: "Nombre",
                unique: true,
                filter: "[Nombre] IS NOT NULL");
        }
    }
}
