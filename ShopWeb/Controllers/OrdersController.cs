namespace ShopWeb.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShopWeb.Data;
    using ShopWeb.Data.Repository;

    [Authorize]
    public class OrdersController : Controller
    {
        #region Attributtes
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        #endregion

        #region Costructor
        public OrdersController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index()
        {
            var model = await orderRepository.GetOrdersAsync(User.Identity.Name);

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await orderRepository.GetDetailsTempAsunc(User.Identity.Name);
            return View(model);
        } 
        #endregion
    }
}