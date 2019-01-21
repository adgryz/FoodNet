using FoodNet.Model.Domain;
using System.Collections.Generic;

namespace FoodNet.UI.Data.Interfaces
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<ProductCategory> GetAllCategories();
        void AddProduct(Product product);
        void Save();
    }
}
