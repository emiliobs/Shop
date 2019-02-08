namespace ShopWeb.Data
{
    using ShopWeb.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class SeedDb
    {
        private readonly DataContext db;
        private Random random;
        public SeedDb(DataContext context)
        {
            this.db = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.db.Database.EnsureCreatedAsync();

            if (!this.db.Products.Any())
            {
                this.AddProduct("iPhone x");
                this.AddProduct("Magic Mouse");
                this.AddProduct("IWatch series 4");
                await db.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            db.Products.Add(new Product()
            {
                Name = name,
                Price = random.Next(100),
                IsAvailable = true,
                Stock = random.Next(100),

            });
        }
    }
}
