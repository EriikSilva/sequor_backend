
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
                    production.StartDateAppointment
                });

            modelBuilder.Entity<models.ProductMaterial>()
                .HasKey(productMaterial => new
                {
                    productMaterial.ProductCode,
                    productMaterial.MaterialCode
                });

            modelBuilder.Entity<models.OrderProduction>().HasData(
                new models.OrderProduction
                {
                    Order = "111",
                    Quantity = 100.00m,
                    ProductCode = "abc"
                },
                new models.OrderProduction
                {
                    Order = "222",
                    Quantity = 200.00m,
                    ProductCode = "def"
                });

            modelBuilder.Entity<models.Product>().HasData(
                new models.Product
                {
                    ProductCode = "abc",
                    ProductDescription = "xxx",
                    Image = "0x000001",
                    CycleTime = 30.3m
                },
                new models.Product
                {
                    ProductCode = "def",
                    ProductDescription = "yyy",
                    Image = "0x00000",
                    CycleTime = 45.5m
                });

            modelBuilder.Entity<models.Material>().HasData(
                new models.Material
                {
                    MaterialCode = "aaa",
                    MaterialDescription = "desc1"
                },
                new models.Material
                {
                    MaterialCode = "bbb",
                    MaterialDescription = "desc2"
                },
                new models.Material
                {
                    MaterialCode = "ccc",
                    MaterialDescription = "desc3"
                });

            modelBuilder.Entity<models.ProductMaterial>().HasData(
                new models.ProductMaterial { ProductCode = "abc", MaterialCode = "aaa", SortOrder = 1 },
                new models.ProductMaterial { ProductCode = "abc", MaterialCode = "bbb", SortOrder = 2 },
                new models.ProductMaterial { ProductCode = "def", MaterialCode = "ccc", SortOrder = 1 },
                new models.ProductMaterial { ProductCode = "def", MaterialCode = "bbb", SortOrder = 2 });
        }
}
}