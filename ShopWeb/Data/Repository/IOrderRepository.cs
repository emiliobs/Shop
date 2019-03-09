namespace ShopWeb.Data.Repository
{
    using ShopWeb.Data.Entities;
    using ShopWeb.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrdersAsync(string userName);
        Task<IQueryable<OrderDetailTemp>> GetDetailsTempAsunc(string userName);
        Task AddItemToOrderAsync(AddItemViewModel model, string userName); 
        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);
        Task DeleteDetailTempAsync(int id);
        Task<bool> ConfirmOrderAsync(string userName);

    }
}
