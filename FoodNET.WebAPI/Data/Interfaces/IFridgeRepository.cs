using System;
using System.Collections.Generic;
using FoodNet.ModelCore.Domain;
using FoodNet.ModelCore.Lookups;

namespace FoodNET.WebAPI.Data.Interfaces
{
    public interface IFridgeRepository
    {
        void AddProduct(FridgeProduct fridgeProduct);
        void DeleteProduct(Guid fridgeProductId);
        IEnumerable<Product> GetAllFridgeProducts(Guid userId);
        void CreateFridgeForUser(Guid userId);
        Fridge GetAnyFridge();
        Fridge GetUserFridge(Guid userId);
    }
}