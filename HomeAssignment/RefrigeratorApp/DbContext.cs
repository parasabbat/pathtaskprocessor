using Microsoft.EntityFrameworkCore;
using RefrigeratorApp.Entities;

namespace RefrigeratorApp
{
    public class RaContext : DbContext
    {

        public RaContext(DbContextOptions<RaContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLog> ProductLogs { get; set; }
    }
}


