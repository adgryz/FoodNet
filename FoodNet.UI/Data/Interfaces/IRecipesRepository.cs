using System.Collections.Generic;
using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using System;

namespace FoodNet.UI.Data.Interfaces
{
    public interface IRecipesRepository
    {
        IEnumerable<Recipe> GetAllRecipes();
        IEnumerable<Product> GetProductsForRecipe(Guid recipeId);
        IEnumerable<RecipeProductLookup> GetRecipeProductLookupsForRecipe(Guid recipeId);
        Boolean ContainsProduct(Recipe recipe, Product product);
        void RemoveRecipe(Recipe recipe);
        void RemoveRecipeProduct(RecipeProduct recipeProduct);
        void AddRecipe(Recipe recipe);
        void Save();
        void AddRecipeProduct(RecipeProduct recipeProduct);
    }
}
