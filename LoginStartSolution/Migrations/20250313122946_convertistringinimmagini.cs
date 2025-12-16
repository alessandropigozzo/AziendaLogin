using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginStartMenu.Migrations
{
    /// <inheritdoc />
    public partial class convertistringinimmagini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "UtentiRuoli");

            migrationBuilder.DropTable(
                name: "Ruoli");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Anagrafiche");

            migrationBuilder.DropTable(
                name: "Immagini");

            migrationBuilder.CreateTable(
                name: "Anagrafiche",
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
                    table.PrimaryKey("PK_Anagrafiche", x => x.IdAnagrafica);
                });

            migrationBuilder.CreateTable(
                name: "Immagini",
                columns: table => new
                {
                    IdImmagine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Img2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Img3 = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Immagini", x => x.IdImmagine);
                });

            migrationBuilder.CreateTable(
                name: "Ruoli",
                columns: table => new
                {
                    IdRuolo = table.Column<int>(type: "int", nullable: false),
                    NomeRuolo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruoli", x => x.IdRuolo);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    IdUtente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAnagrafica = table.Column<int>(type: "int", nullable: false),
                    IdImmagine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.IdUtente);
                    table.ForeignKey(
                        name: "FK_Utenti_Anagrafiche_IdAnagrafica",
                        column: x => x.IdAnagrafica,
                        principalTable: "Anagrafiche",
                        principalColumn: "IdAnagrafica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utenti_Immagini_IdImmagine",
                        column: x => x.IdImmagine,
                        principalTable: "Immagini",
                        principalColumn: "IdImmagine",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UtentiRuoli",
                columns: table => new
                {
                    IdUtenteRuolo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtente = table.Column<int>(type: "int", nullable: false),
                    IdRuolo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtentiRuoli", x => x.IdUtenteRuolo);
                    table.ForeignKey(
                        name: "FK_UtentiRuoli_Ruoli_IdRuolo",
                        column: x => x.IdRuolo,
                        principalTable: "Ruoli",
                        principalColumn: "IdRuolo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtentiRuoli_Utenti_IdUtente",
                        column: x => x.IdUtente,
                        principalTable: "Utenti",
                        principalColumn: "IdUtente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_IdAnagrafica",
                table: "Utenti",
                column: "IdAnagrafica",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utenti_IdImmagine",
                table: "Utenti",
                column: "IdImmagine",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UtentiRuoli_IdRuolo",
                table: "UtentiRuoli",
                column: "IdRuolo");

            migrationBuilder.CreateIndex(
                name: "IX_UtentiRuoli_IdUtente",
                table: "UtentiRuoli",
                column: "IdUtente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UtentiRuoli");

            migrationBuilder.DropTable(
                name: "Ruoli");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "Anagrafiche");

            migrationBuilder.DropTable(
                name: "Immagini");
        }
    }
}
