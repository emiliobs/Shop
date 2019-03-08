namespace ShopWeb.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using ShopWeb.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers() => this.context.Products.Include(p => p.User);

        public IEnumerable<SelectListItem> GetComboProducts()
        {
            var list = context.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(),
               
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Product.....)",
                Value = "0",
            });

            return list;
        }
    }
}
