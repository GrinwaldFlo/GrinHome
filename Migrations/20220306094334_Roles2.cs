using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrinHome.Migrations
{
    public partial class Roles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1066902-71f6-47cd-bed9-67451ebb100e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de24da68-7ba7-4c94-a1f4-8c401fdc21ad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1066902-71f6-47cd-bed9-67451ebb100e", "08549efb-3c1a-43f0-b8ca-e9352802b7ea", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "de24da68-7ba7-4c94-a1f4-8c401fdc21ad", "7f7ff877-0f8c-4934-91f1-37bfa9e1e85d", "User", "USER" });
        }
    }
}
