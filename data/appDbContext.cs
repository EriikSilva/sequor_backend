
using Microsoft.EntityFrameworkCore;

namespace sequor_backend.data
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options){}

        public DbSet<models.Product> Product { get; set; }

        public DbSet<models.OrderProduction> OrderProduction { get; set; }

        public DbSet<models.Production> Production { get; set; }

        public DbSet<models.Material> Material { get; set; }

        public DbSet<models.ProductMaterial> ProductMaterial { get; set; }

        public DbSet<models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<models.Production>()
                .HasKey(production => new
                {
                    production.Id,
                    production.Email,
                    production.Order,
                    production.Date
                });
        }
}
}