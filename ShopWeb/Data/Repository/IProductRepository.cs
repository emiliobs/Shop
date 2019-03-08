namespace ShopWeb.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ShopWeb.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable GetAllWithUsers();
        IEnumerable<SelectListItem> GetComboProducts();
    }
}
