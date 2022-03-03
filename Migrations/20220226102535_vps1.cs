using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrinHome.Migrations
{
    public partial class vps1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopMessageCount",
                table: "Postfixes");

            migrationBuilder.AlterColumn<string>(
                name: "TopSendersSize",
                table: "Postfixes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TopSendersCount",
                table: "Postfixes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TopRecipientsSize",
                table: "Postfixes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TopRecipientsCount",
                table: "Postfixes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TopMessageReceived",
                table: "Postfixes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TopMessageDelivery",
                table: "Postfixes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "MessageRejectDetail",
                table: "Postfixes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Postfixes",
                keyColumn: "TopSendersSize",
                keyValue: null,
                column: "TopSendersSize",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TopSendersSize",
                table: "Postfixes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Postfixes",
                keyColumn: "TopSendersCount",
                keyValue: null,
                column: "TopSendersCount",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TopSendersCount",
                table: "Postfixes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Postfixes",
                keyColumn: "TopRecipientsSize",
                keyValue: null,
                column: "TopRecipientsSize",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TopRecipientsSize",
                table: "Postfixes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Postfixes",
                keyColumn: "TopRecipientsCount",
                keyValue: null,
                column: "TopRecipientsCount",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TopRecipientsCount",
                table: "Postfixes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Postfixes",
                keyColumn: "TopMessageReceived",
                keyValue: null,
                column: "TopMessageReceived",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TopMessageReceived",
                table: "Postfixes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Postfixes",
                keyColumn: "TopMessageDelivery",
                keyValue: null,
                column: "TopMessageDelivery",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TopMessageDelivery",
                table: "Postfixes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Postfixes",
                keyColumn: "MessageRejectDetail",
                keyValue: null,
                column: "MessageRejectDetail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MessageRejectDetail",
                table: "Postfixes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TopMessageCount",
                table: "Postfixes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
