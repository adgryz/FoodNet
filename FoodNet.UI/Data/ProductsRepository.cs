using FoodNet.DataAccess;
using FoodNet.Model.Domain;
using FoodNet.UI.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FoodNet.UI.Data
{
    public class ProductsRepository : IProductsRepository
    {
        private FoodNetDbContext _context;

        public ProductsRepository(FoodNetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            return _context.ProductCategories.ToList();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
