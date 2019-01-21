using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNET.WebAPI.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace FoodNET.WebAPI.Data
{
    public class ProductsRepository : IProductsRepository
    {
        private FoodNetDbContext _context;
        private IMapper _mapper;

        public ProductsRepository(FoodNetDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Product GetProductById(Guid productId)
        {
            return _context.Products.Where(p => p.Id == productId).FirstOrDefault();
        }

        public IEnumerable<Product> GetAllCategoryProducts(Guid categoryId)
        {
            List<Product> categoryProducts = new List<Product>();
            categoryProducts.AddRange(GetAllBasicCategoryProducts(categoryId));
            categoryProducts.AddRange(GetAllNewCategoryProducts(categoryId));
            return categoryProducts;
        }

        public IEnumerable<Product> GetAllBasicCategoryProducts(Guid categoryId)
        {
            return _context.BasicProducts.Where(p => p.ProductCategoryId == categoryId)
                .Include(p => p.User)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public IEnumerable<Product> GetAllNewCategoryProducts(Guid categoryId)
        {
            return _context.NewProducts.Where(p => p.ProductCategoryId == categoryId)
                .Include(p => p.User)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public IEnumerable<Product> GetAllCategoryProductsForUser(Guid categoryId, Guid userId)
        {
            List<Product> categoryProducts = new List<Product>();
            categoryProducts.AddRange(GetAllBasicCategoryProducts(categoryId));
            categoryProducts.AddRange(GetAllNewCategoryProductsForUser(categoryId, userId));
            return categoryProducts;
        }

        public IEnumerable<Product> GetAllNewCategoryProductsForUser(Guid categoryId, Guid userId)
        {
            return _context.NewProducts.Where(p => p.ProductCategoryId == categoryId && p.UserId == userId)
                .Include(p => p.User)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            allProducts.AddRange(GetAllNewProducts());
            allProducts.AddRange(GetAllBasicProducts());
            return allProducts;
        }

        public IEnumerable<Product> GetAllNewProducts()
        {
            return _context.NewProducts
                .Include(p => p.User)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public IEnumerable<Product> GetAllBasicProducts()
        {
            return _context.BasicProducts
                .Include(p => p.User)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public IEnumerable<Product> GetAllProductsForUser(Guid userId)
        {
            List<Product> productsForUser = new List<Product>();
            productsForUser.AddRange(GetAllBasicProducts());
            productsForUser.AddRange(GetAllNewProductsForUser(userId));
            return productsForUser;
        }

        public IEnumerable<Product> GetAllNewProductsForUser(Guid userId)
        {
            return _context.NewProducts.Where(p => p.UserId == userId)
                .Include(p => p.User)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public IEnumerable<ProductCategory> GetAllCategories()
        {
            return _context.ProductCategories
                .Include(pc => pc.Products)
                .ToList();
        }

        public void AddNewProduct(NewProduct product)
        {
            _context.NewProducts.Add(product);
            if (GetNewProductsByName(product.Name).Count() == 10)
            {
                BasicProduct basicProduct = _mapper.Map<NewProduct, BasicProduct>(product);
                basicProduct.Id = Guid.NewGuid();
                AddBasicProduct(basicProduct);
            }
            Save();
        }

        public void AddBasicProduct(BasicProduct product)
        {
            if (GetBasicProductsByName(product.Name).Count() != 0)
            {
                return;
            }
            _context.BasicProducts.Add(product);
            Save();
        }

        public IEnumerable<Product> GetNewProductsByName(string name)
        {
            return _context.NewProducts.Where(p => p.Name.ToLower().Equals(name.ToLower()))
                .Include(p => p.User)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public IEnumerable<Product> GetBasicProductsByName(string name)
        {
            return _context.BasicProducts.Where(p => p.Name.ToLower().Equals(name.ToLower()))
                .Include(p => p.User)
                .Include(p => p.ProductCategory)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
