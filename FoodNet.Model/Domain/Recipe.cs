using System;
using System.Collections.Generic;

namespace FoodNet.Model.Domain
{
    public class Recipe
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<RecipeProduct> RecipeProducts { get; set; }


        public Recipe DeepCopy()
        {
            Recipe recipeCopy = (Recipe)MemberwiseClone();
            recipeCopy.RecipeProducts = new List<RecipeProduct>(RecipeProducts);
            return recipeCopy;
        }
    }
}
