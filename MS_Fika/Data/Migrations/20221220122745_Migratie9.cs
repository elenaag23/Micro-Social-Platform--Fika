using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS_Fika.Data.Migrations
{
    public partial class Migratie9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_ApplicationUserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ApplicationUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGroup_GroupsGroupId",
                table: "ApplicationUserGroup",
                column: "GroupsGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGroup");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ApplicationUserId",
                table: "Groups",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_ApplicationUserId",
                table: "Groups",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
