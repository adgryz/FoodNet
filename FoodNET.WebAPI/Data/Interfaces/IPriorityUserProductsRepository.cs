using FoodNet.ModelCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNET.WebAPI.Data.Interfaces
{
    public interface IPriorityUserProductsRepository
    {
        IEnumerable<PriorityUserProduct> GetAllPriorityUserProducts();
        void IncrementPriorityCount(Guid userId, Guid productId);
        void Save();
    }
}
