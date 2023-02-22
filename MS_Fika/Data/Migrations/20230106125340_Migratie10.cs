using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS_Fika.Data.Migrations
{
    public partial class Migratie10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGroup");


            migrationBuilder.AddColumn<string>(
                name: "GroupAdminUserId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserInGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInGroups", x => new { x.Id, x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserInGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupAdminUserId",
                table: "Groups",
                column: "GroupAdminUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInGroups_GroupId",
                table: "UserInGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInGroups_UserId",
                table: "UserInGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_GroupAdminUserId",
                table: "Groups",
                column: "GroupAdminUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_GroupAdminUserId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "UserInGroups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupAdminUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupAdminUserId",
                table: "Groups");

            migrationBuilder.CreateTable(
                name: "ApplicationUserGroup",
                columns: table => new
                {
                    GroupUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupsGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGroup", x => new { x.GroupUsersId, x.GroupsGroupId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroup_AspNetUsers_GroupUsersId",
                        column: x => x.GroupUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroup_Groups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

        }

            
    }
}
