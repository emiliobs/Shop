namespace ShopWeb.Data
{
    using Microsoft.AspNetCore.Identity;
    using ShopWeb.Data.Entities;
    using ShopWeb.Helpers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    public class SeedDb
    {
        #region Attributes
        private readonly DataContext contex;
        private readonly IUserHelper userHelper; 
        private readonly Random random;
        #endregion

        #region Constructor
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.contex = context;
            this.userHelper = userHelper;

            this.random = new Random();
        }
        #endregion

        #region Methods
        public async Task SeedAsync()
        {
            await this.contex.Database.EnsureCreatedAsync();

            //aqui miro si existen los roles:
            await userHelper.CheckRoleAsync("Admin");
            await userHelper.CheckRoleAsync("Customer");

            //aqui creo el nuevo usuario admin(principal del sistema)
            var user = await this.userHelper.GetUserByEmailAsync("barrera_emilio@hotmail.com");

            if (user == null)
            {
                user = new User()
                {
                    FirstName = "Emilio",
                    LastName = "Barrera",
                    Email = "barrera_emilio@hotmail.com",
                    UserName = "barrera_emilio@hotmail.com",
                    PhoneNumber = "+44907951284",

                };

                var result = await this.userHelper.AddUserAsycncAsync(user, "Eabs123.");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }

                //aqui addionono el role al los usuarios:
                await userHelper.AddUserToRoleAsync(user, "Admin");
            }

            //aqui le adiciono e role cuando el usuarios ya esta creado:
            var isInRole = await userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await userHelper.AddUserToRoleAsync(user,"Admin");
            }

            if (!this.contex.Products.Any())
            {
                this.AddProduct("iPhone x", user);
                this.AddProduct("Magic Mouse", user);
                this.AddProduct("IWatch series 4", user);

                await contex.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            contex.Products.Add(new Product()
            {
                Name = name,
                Price = random.Next(100),
                IsAvailable = true,
                Stock = random.Next(100),
                User = user,

            });
        } 
        #endregion
    }
}
