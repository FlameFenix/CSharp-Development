using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars_Market.Data.Migrations
{
    public partial class CommentEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AuthorPicture",
                table: "Comment",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorPicture",
                table: "Comment");
        }
    }
}
