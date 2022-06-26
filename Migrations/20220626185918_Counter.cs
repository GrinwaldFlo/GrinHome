using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrinHome.Migrations
{
    public partial class Counter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CounterItems",
                columns: table => new
                {
                    ID = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstDay = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterItems", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CounterValues",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CounterItemID = table.Column<ushort>(type: "smallint unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CounterValues_CounterItems_CounterItemID",
                        column: x => x.CounterItemID,
                        principalTable: "CounterItems",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CounterValues_CounterItemID",
                table: "CounterValues",
                column: "CounterItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CounterValues");

            migrationBuilder.DropTable(
                name: "CounterItems");
        }
    }
}
