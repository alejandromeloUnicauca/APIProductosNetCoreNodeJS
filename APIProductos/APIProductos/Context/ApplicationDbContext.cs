
using APIProductos.Model;
using Microsoft.EntityFrameworkCore;

namespace APIProductos.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) {}

        public virtual DbSet<Product> Product { get; set; }
    }
}
