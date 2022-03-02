using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class CitaonicaContext : DbContext
    {
        public DbSet<Fakultet> Fakulteti { get; set; }

        public DbSet<Predmet> Predmeti { get; set; }

        public DbSet<Knjiga> Knjige { get; set; }

        public DbSet<Skripta> Skripte { get; set; }

        public CitaonicaContext(DbContextOptions options) : base(options)
        {
            
        }

    }
}