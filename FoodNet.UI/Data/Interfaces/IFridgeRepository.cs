using System.Collections.Generic;
using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using System;

namespace FoodNet.UI.Data.Interfaces
{
    public interface IFridgeRepository
    {
        Fridge GetFridge();
        IEnumerable<FridgeProductLookup> GetAllFridgeLookups();
        FridgeProduct GetFridgeProductById(Guid productId);
        void AddProduct(FridgeProduct fridgeProduct);
        void DeleteProduct(Guid fridgeProductId);
        void Save();
    }
}