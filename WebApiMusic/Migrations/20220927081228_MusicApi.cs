using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiMusic.Migrations
{
    public partial class MusicApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "artista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreArtistico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreReal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Entidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "musica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Artista = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Album = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_musica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_musica_artista_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "artista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_musica_ArtistaId",
                table: "musica",
                column: "ArtistaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "musica");

            migrationBuilder.DropTable(
                name: "artista");
        }
    }
}
