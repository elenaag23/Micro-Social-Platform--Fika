using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MS_Fika.Data.Migrations
{
    public partial class Migratie13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_UserAdaugatId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_UserCareAdaugaId",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "UserCareAdaugaId",
                table: "Friends",
                newName: "User2_Id");

            migrationBuilder.RenameColumn(
                name: "UserAdaugatId",
                table: "Friends",
                newName: "User1_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_UserCareAdaugaId",
                table: "Friends",
                newName: "IX_Friends_User2_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_UserAdaugatId",
                table: "Friends",
                newName: "IX_Friends_User1_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_User1_Id",
                table: "Friends",
                column: "User1_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_User2_Id",
                table: "Friends",
                column: "User2_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_User1_Id",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_AspNetUsers_User2_Id",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "User2_Id",
                table: "Friends",
                newName: "UserCareAdaugaId");

            migrationBuilder.RenameColumn(
                name: "User1_Id",
                table: "Friends",
                newName: "UserAdaugatId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_User2_Id",
                table: "Friends",
                newName: "IX_Friends_UserCareAdaugaId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_User1_Id",
                table: "Friends",
                newName: "IX_Friends_UserAdaugatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_UserAdaugatId",
                table: "Friends",
                column: "UserAdaugatId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_AspNetUsers_UserCareAdaugaId",
                table: "Friends",
                column: "UserCareAdaugaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
