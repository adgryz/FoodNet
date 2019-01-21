using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNET.WebAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNET.WebAPI.Data
{
    public class PriorityUserProductsRepository : IPriorityUserProductsRepository
    {
        private FoodNetDbContext _context;

        public PriorityUserProductsRepository(FoodNetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PriorityUserProduct> GetAllPriorityUserProducts()
        {
            return _context.PriorityUserProducts.ToList();
        }

        public void IncrementPriorityCount(Guid userId, Guid productId)
        {
            PriorityUserProduct priorityUserProduct = _context.PriorityUserProducts
                .Where(pup => pup.UserId == userId && pup.ProductId == productId)
                .FirstOrDefault();
            if(priorityUserProduct != null)
            {
                priorityUserProduct.Count += 1;
            }
            else
            {
                priorityUserProduct = new PriorityUserProduct
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ProductId = productId,
                    Count = 1
                };
                _context.PriorityUserProducts.Add(priorityUserProduct);
            }

            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
