namespace ShopWeb.Controllers.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ShopWeb.Data;
    using ShopWeb.Data.Entities;
    using ShopWeb.Helpers;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : Controller
    {
        #region Attributtes
        private readonly IProductRepository productRepository;
        private readonly IUserHelper userHelper;
        #endregion
        #region Constructor
        public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
            this.productRepository = productRepository;
            this.userHelper = userHelper;
        }
        #endregion


        #region Methods

        [HttpGet]
        public IActionResult GetProducts()
        {

            return Ok(this.productRepository.GetAllWithUsers());
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] ShopCommon.Models.Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            //here send the user for create the new product:
            var user = await this.userHelper.GetUserByEmailAsync(product.User.Email);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            //TODO: Upload images
            var entityProduct = new Product
            {
                IsAvailable = product.IsAvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = user
            };

            var newProduct = await this.productRepository.CreateAsync(entityProduct);
            return Ok(newProduct);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] ShopCommon.Models.Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            var oldProduct = await this.productRepository.GetByIdAsync(id);
            if (oldProduct == null)
            {
                return this.BadRequest("Product Id don't exists.");

            }

            //TODO: Upload Images


            //here update the oldPorduct with the new product from the product  mobile:
            oldProduct.IsAvailable = product.IsAvailable;
            oldProduct.LastPurchase = product.LastPurchase;
            oldProduct.LastSale = product.LastSale;
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.Stock = product.Stock;

            var updateProduct = await this.productRepository.UpdateAsync(oldProduct);

            //here return the product like this in the database:

            return Ok(updateProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            //here  look for the product in the database with id from the mobile:
            var product = await this.productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return this.NotFound();
            }

            await this.productRepository.DeleteAsync(product);

            return Ok(product);
        }


        #endregion
    }
}
