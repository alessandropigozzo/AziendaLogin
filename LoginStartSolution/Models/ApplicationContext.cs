using Microsoft.EntityFrameworkCore;
using LoginStartMenu.Models.Entity;


namespace LoginStartMenu.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Utente> Utente { get; set; }
        public DbSet<Ruolo> Ruolo { get; set; }
    }

}

