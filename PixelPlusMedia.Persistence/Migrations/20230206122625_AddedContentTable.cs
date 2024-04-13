using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPlusMedia.Persistence.Migrations
{
    public partial class AddedContentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false),
                    WelcomeMessage = table.Column<string>(type: "text", nullable: false),
                    ThankyouMessage = table.Column<string>(type: "text", nullable: false),
                    CampaignMessage = table.Column<string>(type: "text", maxLength: 35, nullable: false),
                    Tnc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.ContentId);
                });

            migrationBuilder.CreateTable(
                name: "SubMessage",
                columns: table => new
                {
                    SubMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMessage", x => x.SubMessageId);
                    table.ForeignKey(
                        name: "FK_SubMessage_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubMessage_ContentId",
                table: "SubMessage",
                column: "ContentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubMessage");

            migrationBuilder.DropTable(
                name: "Content");
        }
    }
}
