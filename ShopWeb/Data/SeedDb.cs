namespace ShopWeb.Data
{
    using Microsoft.AspNetCore.Identity;
    using ShopWeb.Data.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    public class SeedDb
    {
        private readonly DataContext db;
        private readonly UserManager<User> userManager;
        private readonly Random random;
        public SeedDb(DataContext context, UserManager<User> userManager)
        {
            this.db = context;
            this.userManager = userManager;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.db.Database.EnsureCreatedAsync();

            //aqui creo el nuevo usuario admin(principal del sistema)
            var user = await this.userManager.FindByEmailAsync("barrera_emilio@hotmail.com");

            if (user == null)
            {
                user = new User()
                {
                   FirstName = "Emilio",
                   LastName = "Barrera",
                   Email = "barrera_emilio@hotmail.com",
                   UserName = "barrera_emilio@hotmail.com",
                   PhoneNumber ="+44907951284",

                };

                var result = await this.userManager.CreateAsync(user,"Eabs123.");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }
            }

            if (!this.db.Products.Any())
            {
                this.AddProduct("iPhone x", user);
                this.AddProduct("Magic Mouse", user);
                this.AddProduct("IWatch series 4", user);

                await db.SaveChangesAsync();
            }
        }

        private void AddProduct(string name , User user)
        {
            db.Products.Add(new Product()
            {
                Name = name,
                Price = random.Next(100),
                IsAvailable = true,
                Stock = random.Next(100),
                User = user,

            });
        }
    }
}
