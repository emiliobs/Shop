namespace ShopWeb.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ShopWeb.Data;
    using ShopWeb.Data.Entities;
    using ShopWeb.Helpers;
    using ShopWeb.Models;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;


  
    public class ProductsController : Controller
    {
        #region Attributes
        private readonly IProductRepository productRepository;
        private readonly IUserHelper userHelper;
        #endregion

        #region Costructor
        public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
            this.productRepository = productRepository;
            this.userHelper = userHelper;
        }
        #endregion

        #region Methods

        // GET: Products       
        public IActionResult Index()
        {
            return View(this.productRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await this.productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                                  
                string path = string.Empty;
                if (productViewModel.ImageFile != null && productViewModel.ImageFile.Length > 0)
                {
                    //variable para que no se repita una aimagen
                    var guid = Guid.NewGuid().ToString(); 
                    var file = $"{guid}.jpg";

                    //aqui es la ruta del servido local  nombre original del la foto
                    //path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Products",
                    //                    productViewModel.ImageFile.FileName);

                    //aqui es la ruta del servido local  nombre aleatorio de la foto
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Products",file);


                    //Aqui la publico
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await productViewModel.ImageFile.CopyToAsync(stream);
                    }

                   // path = $"~/images/Products/{productViewModel.ImageFile.FileName}";
                    path = $"~/images/Products/{file}";
                }


                var product = this.ToProduct(productViewModel, path);

               
                //product.User = await this.userHelper.GetUserByEmailAsync("barrera_emilio@hotmail.com");
                product.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                await this.productRepository.CreateAsync(product);

                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        private Product ToProduct(ProductViewModel productViewModel, string path)
        {
            return new Product()
            {
                Id = productViewModel.Id,
                ImageUrl = path,
                IsAvailable = productViewModel.IsAvailable,
                LastPurchase = productViewModel.LastPurchase,
                LastSale = productViewModel.LastSale,
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Stock = productViewModel.Stock,
                User = productViewModel.User,

            };
        }
                                                                         
        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await this.productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = this.ToProductViewModel(product); 

            return View(productViewModel);
        }

        private ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel()
            {
                Id =  product.Id,
                ImageUrl =  product.ImageUrl,
                IsAvailable =  product.IsAvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User,
            };
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var path = productViewModel.ImageUrl;
                   
                    if (productViewModel.ImageFile != null && productViewModel.ImageFile.Length > 0)
                    {
                        var guid =  Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Products",file);


                        using (var stream = new FileStream(path, FileMode.Create))
                        {

                            await productViewModel.ImageFile.CopyToAsync(stream);

                        }
                            
                        path = $"~/images/Products/{file}";

                }

                    var product = this.ToProduct(productViewModel, path);

                    
                    //product.User = await this.userHelper.GetUserByEmailAsync("barrera_emilio@hotmail.com");
                    product.User = await this.userHelper.GetUserByEmailAsync(User.Identity.Name);

                    await this.productRepository.UpdateAsync(product);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.productRepository.ExistAsync(productViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await this.productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product product = await this.productRepository.GetByIdAsync(id);

            await this.productRepository.DeleteAsync(product);


            return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.ProductId == id);
        //} 

        #endregion
    }
}
