using System.Collections.Generic;
using FoodNet.ModelCore.Domain;
using System;

namespace FoodNET.WebAPI.Data.Interfaces
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAllCategoryProducts(Guid categoryId);
        IEnumerable<Product> GetAllBasicCategoryProducts(Guid categoryId);
        IEnumerable<Product> GetAllNewCategoryProducts(Guid categoryId);

        IEnumerable<Product> GetAllCategoryProductsForUser(Guid categoryId, Guid userId);
        IEnumerable<Product> GetAllNewCategoryProductsForUser(Guid categoryId, Guid userId);

        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllNewProducts();
        IEnumerable<Product> GetAllBasicProducts();

        IEnumerable<Product> GetAllProductsForUser(Guid userId);
        IEnumerable<Product> GetAllNewProductsForUser(Guid userId);

        Product GetProductById(Guid productId);

        IEnumerable<ProductCategory> GetAllCategories();
        void AddNewProduct(NewProduct product);
        void AddBasicProduct(BasicProduct product);

        IEnumerable<Product> GetNewProductsByName(string name);
        IEnumerable<Product> GetBasicProductsByName(string name);
        void Save();
    }
}