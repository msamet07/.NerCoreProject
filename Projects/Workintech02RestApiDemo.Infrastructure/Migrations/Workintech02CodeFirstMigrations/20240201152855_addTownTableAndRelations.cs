using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workintech02RestApiDemo.Infrastructure.Migrations.Workintech02CodeFirstMigrations
{
    /// <inheritdoc />
    public partial class addTownTableAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "APP",
                table: "Movie",
                newName: "ID");

            migrationBuilder.CreateTable(
                name: "Town",
                schema: "APP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Town", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Town_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "APP",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Town_CityId",
                schema: "APP",
                table: "Town",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Town",
                schema: "APP");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "APP",
                table: "Movie",
                newName: "Id");
        }
    }
}
