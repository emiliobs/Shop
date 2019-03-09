namespace ShopWeb.Data.Repository
{
    using Microsoft.EntityFrameworkCore;
    using ShopWeb.Data.Entities;
    using ShopWeb.Helpers;
    using ShopWeb.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        #region Atributtes
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        #endregion

        #region Constructor
        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            this.context = context;
            this.userHelper = userHelper;
        }


        #endregion

        #region Methods
        public async Task<IQueryable<Order>> GetOrdersAsync(string userName)
        {
            var user = await userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            //si el usurio es admin
            if (await userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return context.Orders.Include(u => u.User)
                                     .Include(o => o.Items)
                                     .ThenInclude(p => p.Product)
                                     .OrderByDescending(o => o.OrderDate);
            }

            //si el usuario es user:
            return context.Orders.Include(o => o.Items).ThenInclude(p => p.Product).Where(u => u.User.Equals(user))
                           .OrderByDescending(o => o.OrderDate);

        }

        public async Task<IQueryable<OrderDetailTemp>> GetDetailsTempAsunc(string userName)
        {
            var user = await userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return context.OrderDetailTemps.Include(o => o.Product).Where(o => o.User.Equals(user)).OrderBy(o => o.Product.Name);
        }

        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            var user = await userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }

            var product = await context.Products.FindAsync(model.ProductId);
            if (product == null)
            {
                return;
            }

            var orderDeetailTemp = await context.OrderDetailTemps.Where(odt => odt.User.Equals(user) && odt.Product.Equals(product))
                                   .FirstOrDefaultAsync();
            if (orderDeetailTemp == null)
            {
                orderDeetailTemp = new OrderDetailTemp()
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity,
                    User = user,
                };

                context.OrderDetailTemps.Add(orderDeetailTemp);
            }
            else
            {
                orderDeetailTemp.Quantity += model.Quantity;
                context.OrderDetailTemps.Update(orderDeetailTemp);
            }

            await context.SaveChangesAsync();
        }

        public async Task ModifyOrderDetailTempQuantityAsync(int id, double quantity)
        {
            var orderDetailsTemp = await context.OrderDetailTemps.FindAsync(id);

            if (orderDetailsTemp == null)
            {
                return;
            }

            orderDetailsTemp.Quantity += quantity;
            if (orderDetailsTemp.Quantity > 0)
            {
                context.OrderDetailTemps.Update(orderDetailsTemp);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var orderDetailsTemp = await context.OrderDetailTemps.FindAsync(id);
            if (orderDetailsTemp == null)
            {
                return;
            }

            context.OrderDetailTemps.Remove(orderDetailsTemp);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            var user = await userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var orderDetailtemp = await context.OrderDetailTemps.Include(p => p.Product).Where(u => u.User.Equals(user))
                                              .ToListAsync();
            if (orderDetailtemp == null || orderDetailtemp.Count == 0)
            {
                return false;
            }

            //aqui no uso forech u so programación funcional:
            var orderDetails = orderDetailtemp.Select(odt =>  new OrderDetail()
            {
               Price = odt.Price,
               Product = odt.Product,
               Quantity = odt.Quantity
            }).ToList();

            var order = new Order()
            {
               OrderDate = DateTime.UtcNow,
               User = user,
               Items = orderDetails,
            };

            context.Orders.Add(order);
            context.OrderDetailTemps.RemoveRange(orderDetailtemp);
            await context.SaveChangesAsync();

            return true;

        }

        #endregion
    }
}
