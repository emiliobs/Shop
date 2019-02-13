namespace ShopWeb.Data
{
    using ShopWeb.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class Repository : IRepository
    {
        #region Attributtes
        //aqui inyecto y esta disponible para toda la clase:
        private readonly DataContext context;
        #endregion

        #region Constructor
        public Repository(DataContext context)
        {
            this.context = context;
        }
        #endregion

        #region Methods

        public IEnumerable<Product> GetProducts() => this.context.Products.OrderBy(p => p.Name);

        public Product GetProduct(int id) => this.context.Products.Find(id);

        public void AddProduct(Product product) => this.context.Products.Add(product);

        public void UpdateProduct(Product product) => this.context.Products.Update(product);

        public void DeleteProduct(Product product) => this.context.Products.Remove(product);

        public async Task<bool> SaveAllAsync() => await this.context.SaveChangesAsync() > 0;

        public bool ProductExists(int id) => this.context.Products.Any(p => p.Equals(id));

        #endregion
    }
}
