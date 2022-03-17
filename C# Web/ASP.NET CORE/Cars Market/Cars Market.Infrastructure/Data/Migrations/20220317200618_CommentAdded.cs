using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars_Market.Data.Migrations
{
    public partial class CommentAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Sellers_SellerId",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "SendFromEmail",
                table: "Messages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "SellerId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CarId",
                table: "Comment",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Sellers_SellerId",
                table: "Messages",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Sellers_SellerId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "SendFromEmail",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "SellerId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Sellers_SellerId",
                table: "Messages",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }
    }
}
