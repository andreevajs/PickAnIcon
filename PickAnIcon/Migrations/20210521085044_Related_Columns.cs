using Microsoft.EntityFrameworkCore.Migrations;

namespace PickAnIcon.Migrations
{
    public partial class Related_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IconParts_Icons_IconID",
                table: "IconParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Icons_Users_UserID",
                table: "Icons");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Icons",
                newName: "OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Icons_UserID",
                table: "Icons",
                newName: "IX_Icons_OwnerID");

            migrationBuilder.AlterColumn<int>(
                name: "IconID",
                table: "IconParts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IconParts_Icons_IconID",
                table: "IconParts",
                column: "IconID",
                principalTable: "Icons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Icons_Users_OwnerID",
                table: "Icons",
                column: "OwnerID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IconParts_Icons_IconID",
                table: "IconParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Icons_Users_OwnerID",
                table: "Icons");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Icons",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Icons_OwnerID",
                table: "Icons",
                newName: "IX_Icons_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "IconID",
                table: "IconParts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_IconParts_Icons_IconID",
                table: "IconParts",
                column: "IconID",
                principalTable: "Icons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Icons_Users_UserID",
                table: "Icons",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
