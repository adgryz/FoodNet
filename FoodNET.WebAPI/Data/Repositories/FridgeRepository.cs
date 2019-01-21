using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNet.ModelCore.Lookups;
using FoodNET.WebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodNET.WebAPI.Data
{
    public class FridgeRepository : IFridgeRepository
    {
        private FoodNetDbContext _context;

        public FridgeRepository(FoodNetDbContext context)
        {
            _context = context;
        }

        public void CreateFridgeForUser(Guid userId)
        {
            Fridge fridge = new Fridge
            {
                Id = Guid.NewGuid(),
                UserId = userId,
            };

            _context.Fridges.Add(fridge);
            Save();
        }

        public Fridge GetAnyFridge()
        {
            return _context.Fridges
                .Include(f => f.FridgeProducts)
                .First();
        }

        public IEnumerable<Product> GetAllFridgeProducts(Guid userId)
        {
            var fridgeId = GetUserFridge(userId).Id;
            return _context.FridgeProducts.Where(fp => fp.FridgeId == fridgeId)
                .Join(_context.Products, fp => fp.ProductId, p => p.Id, (rp, p) => p)
                .ToList();
        }

        public Fridge GetUserFridge(Guid userId)
        {
            var fridges = _context.Fridges.ToList();
            var result = _context.Fridges.Where(f => f.UserId == userId)
                //.Include(f => f.FridgeProducts) - TODO ( wywala w postmanie)
                .Single();
            return result;
        }

        public void DeleteProduct(Guid productId)
        {
            var fridgeProduct = _context.FridgeProducts.Where(fr => fr.ProductId == productId).FirstOrDefault();
            if (fridgeProduct != null)
            {
                _context.FridgeProducts.Remove(fridgeProduct);
                Save();
            }
        }

        public void AddProduct(FridgeProduct fridgeProduct)
        {
            if (!_context.FridgeProducts.Any(fp => fp.Id == fridgeProduct.Id))
            {
                _context.FridgeProducts.Add(fridgeProduct);
                Save();
            }
        }

        private void Save()
        {
            _context.SaveChanges();
        }

        private FridgeProduct GetFridgeProductById(Guid fridgeProductId)
        {
            return _context.FridgeProducts.Where(fp => fp.Id == fridgeProductId).Single();
        }
    }
}
