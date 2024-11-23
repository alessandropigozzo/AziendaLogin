using Microsoft.EntityFrameworkCore;
using LoginStartSolution.Models;
using LoginStartSolution.Models.LoginModels;


namespace LoginStartSolution.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<RegistrationViewModel> Registrazione { get; set; }
    }

}

