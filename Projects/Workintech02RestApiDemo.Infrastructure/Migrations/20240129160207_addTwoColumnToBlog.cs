using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workintech02RestApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class addTwoColumnToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Blog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Blog");
        }
    }
}
