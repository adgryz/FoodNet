using System;
using System.Collections.Generic;

namespace FoodNet.ModelCore.Domain
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<FridgeProduct> FridgeProducts { get; set; }
        public List<RecipeProduct> RecipeProducts { get; set; }

        public Guid ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
