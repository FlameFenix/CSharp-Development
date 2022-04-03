using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars_Market.Data.Migrations
{
    public partial class CarMorePicturesAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPicture_Cars_CarId",
                table: "CarPicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarPicture",
                table: "CarPicture");

            migrationBuilder.RenameTable(
                name: "CarPicture",
                newName: "CarPictures");

            migrationBuilder.RenameIndex(
                name: "IX_CarPicture_CarId",
                table: "CarPictures",
                newName: "IX_CarPictures_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarPictures",
                table: "CarPictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPictures_Cars_CarId",
                table: "CarPictures",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPictures_Cars_CarId",
                table: "CarPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarPictures",
                table: "CarPictures");

            migrationBuilder.RenameTable(
                name: "CarPictures",
                newName: "CarPicture");

            migrationBuilder.RenameIndex(
                name: "IX_CarPictures_CarId",
                table: "CarPicture",
                newName: "IX_CarPicture_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarPicture",
                table: "CarPicture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPicture_Cars_CarId",
                table: "CarPicture",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
