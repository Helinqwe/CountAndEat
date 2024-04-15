using CoundAndEat.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoundAndEat.Api.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recept> Recepts { get; set; }
        public DbSet<Ingridient> Ingridients { get; set;}
        public DbSet<ReceptIngridient> ReceptIngridients { get;set; }
        public DbSet<ReceptImages> ReceptImages { get; set; }
        public DbSet<ReceptDescription> ReceptDescriptions { get; set; }
        public DbSet<TokenInfo> TokenInfo { get; set; }
    }
}
