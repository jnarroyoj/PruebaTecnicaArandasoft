using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoAranda.Infrastructure.Migrations
{
    public partial class IndiceUnicoNombreProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0860d2dc-bbd5-4d76-a22e-d88d04f463ff");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Productos",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0860d2dc-bbd5-4d76-a22e-d88d04f463ff", 0, "e0feed4c-acff-4ff5-b8db-eddc8f75eb25", null, false, false, null, null, "ADMIN", "AQAAAAEAACcQAAAAEDOjXV9veWQXWetLloZgmhw3gV/M8GMXSut9FnGYNHxXDnuG47V/oT1kFGG9TaTkTA==", null, false, "f409afe5-537f-494a-b4f9-da25a1747830", false, "admin" });
        }
    }
}
