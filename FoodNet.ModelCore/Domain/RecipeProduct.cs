using System;

namespace FoodNet.ModelCore.Domain
{
    public class RecipeProduct
    {
        public Guid Id { get; set; }

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
