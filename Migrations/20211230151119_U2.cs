using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrinHome.Migrations
{
    public partial class U2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "LimitMax",
                table: "ValueDefinitions",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "LimitMin",
                table: "ValueDefinitions",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitMax",
                table: "ValueDefinitions");

            migrationBuilder.DropColumn(
                name: "LimitMin",
                table: "ValueDefinitions");
        }
    }
}
