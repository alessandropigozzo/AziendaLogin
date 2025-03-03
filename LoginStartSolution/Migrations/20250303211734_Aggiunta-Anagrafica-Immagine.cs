using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginStartMenu.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaAnagraficaImmagine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Anagrafica",
               columns: table => new
               {
                   IdAnagrafica = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Nazionalita = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Eta = table.Column<int>(type: "int", nullable: false),
                   Via = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Indirizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Cap = table.Column<int>(type: "int", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Anagrafica", x => x.IdAnagrafica);
               });

            migrationBuilder.CreateTable(
                name: "Immagine",
                columns: table => new
                {
                    IdImmagine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Immagine", x => x.IdImmagine);
                });

            migrationBuilder.CreateTable(
                name: "UtenteAnagrafica",
                columns: table => new
                {
                    IdUtenteAnagrafica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    IdAnagrafica = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtenteAnagrafica", x => x.IdUtenteAnagrafica);
                    table.ForeignKey(
                        name: "FK_UtenteAnagrafica_Anagrafica_IdAnagrafica",
                        column: x => x.IdAnagrafica,
                        principalTable: "Anagrafica",
                        principalColumn: "IdAnagrafica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtenteAnagrafica_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UtenteImmagine",
                columns: table => new
                {
                    IdUtenteImmagine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    IdImmagine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtenteImmagine", x => x.IdUtenteImmagine);
                    table.ForeignKey(
                        name: "FK_UtenteImmagine_Immagine_IdImmagine",
                        column: x => x.IdImmagine,
                        principalTable: "Immagine",
                        principalColumn: "IdImmagine",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtenteImmagine_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UtenteAnagrafica_IdAnagrafica",
                table: "UtenteAnagrafica",
                column: "IdAnagrafica");

            migrationBuilder.CreateIndex(
                name: "IX_UtenteAnagrafica_IdUtente",
                table: "UtenteAnagrafica",
                column: "IdUtente");

            migrationBuilder.CreateIndex(
                name: "IX_UtenteImmagine_IdImmagine",
                table: "UtenteImmagine",
                column: "IdImmagine");

            migrationBuilder.CreateIndex(
                name: "IX_UtenteImmagine_IdUtente",
                table: "UtenteImmagine",
                column: "IdUtente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "UtenteAnagrafica");

            migrationBuilder.DropTable(
                name: "UtenteImmagine");

            migrationBuilder.DropTable(
                name: "Anagrafica");

            migrationBuilder.DropTable(
                name: "Immagine");
        }
    }
}
