namespace ShopWeb.Data
{
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

    }
}
