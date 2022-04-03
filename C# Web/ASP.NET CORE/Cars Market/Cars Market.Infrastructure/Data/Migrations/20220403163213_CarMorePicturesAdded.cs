using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars_Market.Data.Migrations
{
    public partial class CarMorePicturesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "CarPicture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MainPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SecondPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ThirdPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FourthPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPicture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarPicture_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarPicture_CarId",
                table: "CarPicture",
                column: "CarId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarPicture");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Cars",
                type: "varbinary(max)",
                maxLength: 2048000,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
