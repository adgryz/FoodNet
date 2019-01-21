using System;
using System.Collections.Generic;
using FoodNet.ModelCore.Domain;
using FoodNet.ModelCore.Lookups;

namespace FoodNET.WebAPI.Data.Interfaces
{
    public interface IRecipesRepository
    {
        void AddRecipe(Recipe recipe);
        bool ContainsProduct(Guid recipeId, Guid productId);
        Recipe GetRecipeById(Guid recipeId);
        IEnumerable<Recipe> GetAllPublicPlusUserRecipes(Guid userId, List<Guid> tags);
        IEnumerable<Recipe> GetAllUserRecipes(Guid userId, List<Guid> tags);
        IEnumerable<Product> GetProductsForRecipe(Guid recipeId);
        IEnumerable<RecipeProductLookup> GetRecipeProductLookupsForRecipe(Guid recipeId);
        void UpdateRecipeSimpleValues(Recipe recipe);
        void AddProduct(RecipeProduct recipeProduct);
        void RemoveRecipeProduct(RecipeProduct recipeProduct);
        void RemoveRecipe(Guid id);
        void RemoveProductById(Guid id);
        bool ContainsTag(Guid recipeId, Guid tagId);
        void AddTag(RecipeTag recipeTag);
        void RemoveRecipeTag(RecipeTag recipeTag);
        void RemoveTagById(Guid tagId);
    }
}