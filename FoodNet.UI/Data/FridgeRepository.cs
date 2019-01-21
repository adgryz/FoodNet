using FoodNet.DataAccess;
using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using FoodNet.UI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodNet.UI.Data
{
    public class FridgeRepository : IFridgeRepository
    {
        private FoodNetDbContext _context;

        public FridgeRepository(FoodNetDbContext context)
        {
            _context= context;
        }

        public Fridge GetFridge()
        {
           return  _context.Fridges.Single();
        }

        public IEnumerable<FridgeProductLookup> GetAllFridgeLookups()
        {
                return (from fp in _context.FridgeProducts
                              join p in _context.Products
                              on fp.ProductId equals p.Id
                              select new FridgeProductLookup {
                                  ProductId = p.Id,
                                  FridgeProductId = fp.Id,
                                  Name = p.Name,
                                  Description = p.Description,
                              } ).ToList();
        }

        public FridgeProduct GetFridgeProductById(Guid fridgeProductId)
        {
            return _context.FridgeProducts.Where(fp => fp.Id == fridgeProductId).Single();
        }

        public void DeleteProduct(Guid fridgeProductId)
        {
            FridgeProduct delProduct = GetFridgeProductById(fridgeProductId);
            _context.FridgeProducts.Remove(delProduct);
        }

        public void AddProduct(FridgeProduct fridgeProduct)
        {
            _context.FridgeProducts.Add(fridgeProduct);
        }


        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
