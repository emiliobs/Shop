namespace ShopWeb.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ShopWeb.Data.Entities;
    public interface IRepository
    {
        void AddProduct(Product product);

        void DeleteProduct(Product product);

        Product GetProduct(int id);

        IEnumerable<Product> GetProducts();

        bool ProductExists(int id);

        Task<bool> SaveAllAsync();

        void UpdateProduct(Product product);
    }
}