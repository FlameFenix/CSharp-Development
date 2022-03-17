using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars_Market.Data.Migrations
{
    public partial class comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Cars_CarId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_CarId",
                table: "Comments",
                newName: "IX_Comments_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Cars_CarId",
                table: "Comments",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Cars_CarId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CarId",
                table: "Comment",
                newName: "IX_Comment_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Cars_CarId",
                table: "Comment",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
