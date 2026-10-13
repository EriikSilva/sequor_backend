
using Microsoft.EntityFrameworkCore;

namespace sequor_backend.data
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options){}

        public DbSet<models.Product> Product { get; set; }

        public DbSet<models.Production> OrderProduction { get; set; }

        public DbSet<models.Production> Production { get; set; }

        public DbSet<models.Material> Material { get; set; }

        public DbSet<models.ProductMaterial> ProductMaterial { get; set; }

        public DbSet<models.User> Users { get; set; }
}
}