using System;

namespace FoodNet.ModelCore.Lookups
{
    public class RecipeProductLookup
    {
        public Guid ProductId { get; set; }
        public Guid RecipeProductId { get; set; }
        public string ProductName { get; set; }
    }
}
