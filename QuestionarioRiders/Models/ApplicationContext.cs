using Microsoft.EntityFrameworkCore;
using QuestionarioRiders.Models.LoginModels;

namespace QuestionarioRiders.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<RegistrationViewModel> Registrazione { get; set; }
    }

}

