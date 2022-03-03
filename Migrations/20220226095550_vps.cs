using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrinHome.Migrations
{
    public partial class vps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Postfixes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DatePostfix = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Received = table.Column<int>(type: "int", nullable: false),
                    Delivered = table.Column<int>(type: "int", nullable: false),
                    Forwarded = table.Column<int>(type: "int", nullable: false),
                    Deferred = table.Column<int>(type: "int", nullable: false),
                    Bounced = table.Column<int>(type: "int", nullable: false),
                    Rejected = table.Column<int>(type: "int", nullable: false),
                    RejectedWarning = table.Column<int>(type: "int", nullable: false),
                    Held = table.Column<int>(type: "int", nullable: false),
                    Discarded = table.Column<int>(type: "int", nullable: false),
                    BytesReceived = table.Column<int>(type: "int", nullable: false),
                    BytesDelivered = table.Column<int>(type: "int", nullable: false),
                    Senders = table.Column<int>(type: "int", nullable: false),
                    SendingHostDomain = table.Column<int>(type: "int", nullable: false),
                    Recipients = table.Column<int>(type: "int", nullable: false),
                    RecipientHostDomain = table.Column<int>(type: "int", nullable: false),
                    TopMessageDelivery = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopMessageReceived = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopMessageCount = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopSendersCount = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopRecipientsCount = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopSendersSize = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TopRecipientsSize = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageRejectDetail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SmtpdWarnings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postfixes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Postfixes");
        }
    }
}
