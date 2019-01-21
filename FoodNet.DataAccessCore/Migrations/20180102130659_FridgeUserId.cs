using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FoodNet.DataAccessCore.Migrations
{
    public partial class FridgeUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_AspNetUsers_UserId",
                table: "Fridges");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_UserId",
                table: "Fridges");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Fridges",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Fridges",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_UserId1",
                table: "Fridges",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_AspNetUsers_UserId1",
                table: "Fridges",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_AspNetUsers_UserId1",
                table: "Fridges");

            migrationBuilder.DropIndex(
                name: "IX_Fridges_UserId1",
                table: "Fridges");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Fridges");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Fridges",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_UserId",
                table: "Fridges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_AspNetUsers_UserId",
                table: "Fridges",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
