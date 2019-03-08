namespace ShopWeb.Data.Repository
{
    using Microsoft.EntityFrameworkCore;
    using ShopWeb.Data.Entities;
    using ShopWeb.Helpers;
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
                return context.Orders.Include(o => o.Items).ThenInclude(p => p.Product).OrderByDescending(o => o.OrderDate);
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

        #endregion
    }
}
