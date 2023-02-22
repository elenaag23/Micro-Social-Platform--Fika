using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS_Fika.Data.Migrations
{
    public partial class Migratie8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_UserId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Groups",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_UserId",
                table: "Groups",
                newName: "IX_Groups_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_ApplicationUserId",
                table: "Groups",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_ApplicationUserId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Groups",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_ApplicationUserId",
                table: "Groups",
                newName: "IX_Groups_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_UserId",
                table: "Groups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
