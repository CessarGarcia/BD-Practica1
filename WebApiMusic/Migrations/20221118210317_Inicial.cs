using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMusic.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreArtistico = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NombreReal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "disquera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disquera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtistaDisquera",
                columns: table => new
                {
                    ArtistaId = table.Column<int>(type: "int", nullable: false),
                    DisqueraId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistaDisquera", x => new { x.ArtistaId, x.DisqueraId });
                    table.ForeignKey(
                        name: "FK_ArtistaDisquera_artista_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "artista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistaDisquera_disquera_DisqueraId",
                        column: x => x.DisqueraId,
                        principalTable: "disquera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "musica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Artista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Album = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisqueraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_musica_disquera_DisqueraId",
                        column: x => x.DisqueraId,
                        principalTable: "disquera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistaDisquera_DisqueraId",
                table: "ArtistaDisquera",
                column: "DisqueraId");

            migrationBuilder.CreateIndex(
                name: "IX_musica_DisqueraId",
                table: "musica",
                column: "DisqueraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistaDisquera");

            migrationBuilder.DropTable(
                name: "musica");

            migrationBuilder.DropTable(
                name: "artista");

            migrationBuilder.DropTable(
                name: "disquera");
        }
    }
}
