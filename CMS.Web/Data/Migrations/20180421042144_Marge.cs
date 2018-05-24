using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CMS.Web.Data.Migrations
{
    public partial class Marge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GithubID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeID",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GithubID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstagramID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedinID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwitterID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YoutubeID",
                table: "AspNetUsers");
        }
    }
}
