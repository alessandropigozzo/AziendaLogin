using LoginStartMenu.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace LoginStartMenu.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Costruttore del contesto che prende le opzioni di configurazione
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Definizione delle tabelle nel contesto del database
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Ruolo> Ruoli { get; set; }
        public DbSet<UtenteRuolo> UtentiRuoli { get; set; }
        public DbSet<Anagrafica> Anagrafiche { get; set; }
        public DbSet<Immagine> Immagini { get; set; }

        // Configurazione delle entità e delle relazioni tramite OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Impostazioni per la generazione automatica delle chiavi primarie (auto-incremento)
            modelBuilder.Entity<Utente>()
                .Property(u => u.IdUtente)
                .ValueGeneratedOnAdd();  // Configura l'auto-incremento per IdUtente

            modelBuilder.Entity<Immagine>()
                .Property(i => i.IdImmagine)
                .ValueGeneratedOnAdd();  // Configura l'auto-incremento per IdImmagine

            modelBuilder.Entity<Anagrafica>()
                .Property(a => a.IdAnagrafica)
                .ValueGeneratedOnAdd();  // Configura l'auto-incremento per IdAnagrafica

            // NON configurare il campo 'IdRuolo' per auto-generazione, lasciarlo settato manualmente
            modelBuilder.Entity<Ruolo>()
                .Property(r => r.IdRuolo)
                .ValueGeneratedNever(); // Non generare l'ID automaticamente, lo imposterai manualmente

            // Configurazione della relazione molti-a-molti tra Utente e Ruolo
            modelBuilder.Entity<UtenteRuolo>()
                .HasOne(ur => ur.Utente)
                .WithMany(u => u.UtentiRuoli)
                .HasForeignKey(ur => ur.IdUtente);

            modelBuilder.Entity<UtenteRuolo>()
                .HasOne(ur => ur.Ruolo)
                .WithMany(r => r.UtentiRuoli)
                .HasForeignKey(ur => ur.IdRuolo);

            // Configurazione della relazione uno-a-uno tra Utente e Anagrafica
            modelBuilder.Entity<Utente>()
                .HasOne(u => u.Anagrafica)
                .WithOne(a => a.Utente)
                .HasForeignKey<Utente>(u => u.IdAnagrafica)
                .OnDelete(DeleteBehavior.Cascade); // Imposta il comportamento della cancellazione in cascata

            // Configurazione della relazione uno-a-uno tra Utente e Immagine
            modelBuilder.Entity<Utente>()
                .HasOne(u => u.Immagine)
                .WithOne(i => i.Utente)
                .HasForeignKey<Utente>(i => i.IdImmagine)
                .OnDelete(DeleteBehavior.Cascade); // Imposta il comportamento della cancellazione in cascata
        }
    }
}
