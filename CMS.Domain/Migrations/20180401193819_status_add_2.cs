using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CMS.Domain.Migrations
{
    public partial class status_add_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PostStatus_PostID",
                table: "PostStatus",
                column: "PostID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostStatus_Post_PostID",
                table: "PostStatus",
                column: "PostID",
                principalTable: "Post",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostStatus_Post_PostID",
                table: "PostStatus");

            migrationBuilder.DropIndex(
                name: "IX_PostStatus_PostID",
                table: "PostStatus");
        }
    }
}
