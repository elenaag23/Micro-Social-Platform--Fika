using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS_Fika.Data.Migrations
{
    public partial class Migratie11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    FriendId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User2_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.FriendId);
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_User1_Id",
                        column: x => x.User1_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_User2_Id",
                        column: x => x.User2_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_User1_Id",
                table: "Friends",
                column: "User1_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_User2_Id",
                table: "Friends",
                column: "User2_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");
        }
    }
}
