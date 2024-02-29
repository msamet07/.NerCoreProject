using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Workintech02RestApiDemo.Infrastructure.Migrations.Workintech02CodeFirstMigrations
{
    /// <inheritdoc />
    public partial class updateMovieTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "APP",
                table: "City",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "City",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "APP",
                table: "City",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "APP",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "APP",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "APP",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "APP",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Url",
                schema: "APP",
                table: "Movie");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "APP",
                table: "Movie",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                schema: "APP",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                schema: "APP",
                table: "Movie");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "APP",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "APP",
                table: "Movie",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "APP",
                table: "Movie",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "APP",
                table: "Movie",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "APP",
                table: "Movie",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                schema: "APP",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                schema: "APP",
                table: "City",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ankara" },
                    { 2, "İstanbul" },
                    { 3, "İzmir" }
                });
        }
    }
}
