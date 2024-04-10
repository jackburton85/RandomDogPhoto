using Microsoft.EntityFrameworkCore;
using SPPTest.Domain.Models;

namespace SPPTest.EFDataAccess
{
    public class DogDbContext : DbContext
    {
        public DogDbContext(DbContextOptions<DogDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("WebApiDatabase");
            }
        }
        public DbSet<DogPhoto> DogPhotos { get; set; }

     
    }
}
