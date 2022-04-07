using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars_Market.Data.Migrations
{
    public partial class RelationSellerProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Profiles_ProfileId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_ProfileId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Sellers");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "Profiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_SellerId",
                table: "Profiles",
                column: "SellerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Sellers_SellerId",
                table: "Profiles",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Sellers_SellerId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_SellerId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Profiles");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Sellers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_ProfileId",
                table: "Sellers",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Profiles_ProfileId",
                table: "Sellers",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
