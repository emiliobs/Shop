namespace ShopWeb.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ShopWeb.Data;
    using ShopWeb.Data.Repository;
    using ShopWeb.Helpers;
    using ShopWeb.Models;

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

        public ActionResult AddProduct()
        {
            var model = new AddItemViewModel()
            {
                //aqui armoi la lsita del combo box de products:
                Products = productRepository.GetComboProducts(),
                Quantity = 1,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                await orderRepository.AddItemToOrderAsync(model, User.Identity.Name);
                return RedirectToAction("Create");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("OrderNotFound");
            }

            await orderRepository.DeleteDetailTempAsync(id.Value);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return new NotFoundObjectResult("OrderNotFound");
            }

            await orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, 1);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return new NotFoundObjectResult("OrderNotFound");
            }

            await orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, -1);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> ConfirmOrder()
        {
            var response = await orderRepository.ConfirmOrderAsync(User.Identity.Name);
            if (response)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        #endregion
    }
}