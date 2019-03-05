namespace ShopWeb.Controllers.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShopWeb.Data;
   
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : Controller
    {
        #region Attributtes
        private readonly IProductRepository productRepository; 
        #endregion
        #region Constructor
        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        #endregion


        #region Methods

        [HttpGet]
        public IActionResult GetProducts()
        {
            
            return Ok(this.productRepository.GetAllWithUsers()); 
        }

        #endregion
    }
}
