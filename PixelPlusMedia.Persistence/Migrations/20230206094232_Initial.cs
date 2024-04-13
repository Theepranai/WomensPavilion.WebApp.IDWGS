using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPlusMedia.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultMessage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    MediaUrl = table.Column<string>(type: "text", nullable: false),
                    IsApprove = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DateRegistered = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDetails");
        }
    }
}
