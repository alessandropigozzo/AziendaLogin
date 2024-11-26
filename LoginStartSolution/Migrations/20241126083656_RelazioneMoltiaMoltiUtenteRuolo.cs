using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginStartMenu.Migrations
{
    /// <inheritdoc />
    public partial class RelazioneMoltiaMoltiUtenteRuolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Utente",
                table: "Utente");

            migrationBuilder.RenameTable(
                name: "Utente",
                newName: "Utenti");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utenti",
                table: "Utenti",
                column: "IdUtente");

            migrationBuilder.CreateTable(
                name: "Ruoli",
                columns: table => new
                {
                    IdRuolo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeRuolo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruoli", x => x.IdRuolo);
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utenti",
                table: "Utenti");

            migrationBuilder.RenameTable(
                name: "Utenti",
                newName: "Utente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utente",
                table: "Utente",
                column: "IdUtente");
        }
    }
}
