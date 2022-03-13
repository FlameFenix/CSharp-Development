using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars_Market.Data.Migrations
{
    public partial class sellersUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Messages_MessageId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_MessageId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Sellers");

            migrationBuilder.RenameColumn(
                name: "SellerEmail",
                table: "Messages",
                newName: "SendToEmail");

            migrationBuilder.AddColumn<string>(
                name: "SendFromEmail",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendFromEmail",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "SendToEmail",
                table: "Messages",
                newName: "SellerEmail");

            migrationBuilder.AddColumn<Guid>(
                name: "MessageId",
                table: "Sellers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_MessageId",
                table: "Sellers",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Messages_MessageId",
                table: "Sellers",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
