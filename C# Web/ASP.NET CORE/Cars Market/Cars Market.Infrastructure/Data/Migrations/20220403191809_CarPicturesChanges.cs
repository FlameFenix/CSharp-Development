using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars_Market.Data.Migrations
{
    public partial class CarPicturesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarPictures_CarId",
                table: "CarPictures");

            migrationBuilder.DropColumn(
                name: "FourthPicture",
                table: "CarPictures");

            migrationBuilder.DropColumn(
                name: "MainPicture",
                table: "CarPictures");

            migrationBuilder.DropColumn(
                name: "SecondPicture",
                table: "CarPictures");

            migrationBuilder.RenameColumn(
                name: "ThirdPicture",
                table: "CarPictures",
                newName: "Picture");

            migrationBuilder.AddColumn<byte[]>(
                name: "MainPicture",
                table: "Cars",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_CarPictures_CarId",
                table: "CarPictures",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarPictures_CarId",
                table: "CarPictures");

            migrationBuilder.DropColumn(
                name: "MainPicture",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "CarPictures",
                newName: "ThirdPicture");

            migrationBuilder.AddColumn<byte[]>(
                name: "FourthPicture",
                table: "CarPictures",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "MainPicture",
                table: "CarPictures",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "SecondPicture",
                table: "CarPictures",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_CarPictures_CarId",
                table: "CarPictures",
                column: "CarId",
                unique: true);
        }
    }
}
