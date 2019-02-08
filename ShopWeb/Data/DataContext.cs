namespace ShopWeb.Data
{
    using Microsoft.EntityFrameworkCore;
    using ShopWeb.Data.Entities;

    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
    }
}
