using Microsoft.EntityFrameworkCore;
using LoginStartMenu.Models.Entity;


namespace LoginStartMenu.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Ruolo> Ruoli { get; set; }
        public DbSet<UtenteRuolo> UtentiRuoli { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazione della relazione molti-a-molti
            modelBuilder.Entity<UtenteRuolo>()
                .HasOne(ur => ur.Utente)
                .WithMany(u => u.UtentiRuoli)
                .HasForeignKey(ur => ur.IdUtente);

            modelBuilder.Entity<UtenteRuolo>()
                .HasOne(ur => ur.Ruolo)
                .WithMany(r => r.UtentiRuoli)
                .HasForeignKey(ur => ur.IdRuolo);
        }
    }

}

