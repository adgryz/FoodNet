using FoodNet.DataAccess;
using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using FoodNet.UI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace FoodNet.UI.Data
{
    public class RecipesRepository : IRecipesRepository
    {
        private FoodNetDbContext ctx;

        public RecipesRepository(FoodNetDbContext context)
        {
            ctx = context;
        }

        public IEnumerable<Product> GetProductsForRecipe(Guid recipeId)
        {
           return ctx.RecipeProducts.Where(rp => rp.RecipeId == recipeId)
                .Join(ctx.Products, rp => rp.ProductId, p => p.Id, (rp, p) => p).ToList();
        }

        public Boolean ContainsProduct(Recipe recipe, Product product)
        {
            return ctx.RecipeProducts.Where(rp => rp.ProductId == product.Id && rp.RecipeId == recipe.Id).Any();
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return ctx.Recipes.Include(r => r.RecipeProducts).ToList();
        }

        public IEnumerable<RecipeProductLookup> GetRecipeProductLookupsForRecipe(Guid recipeId)
        {
                              var result = (from rp in ctx.RecipeProducts
                              join p in ctx.Products
                              on rp.ProductId equals p.Id
                              where rp.RecipeId == recipeId
                              select new RecipeProductLookup
                              {
                                  ProductId = p.Id,
                                  ProductName = p.Name,
                                  RecipeProductId = rp.Id
                              }).ToList();
                return result;
        }

        public void AddRecipeProduct(RecipeProduct recipeProduct)
        {
            ctx.RecipeProducts.Add(recipeProduct);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            ctx.Recipes.Remove(recipe);
        }

        public void AddRecipe(Recipe recipe)
        {
            ctx.Recipes.Add(recipe);
        }

        public void RemoveRecipeProduct(RecipeProduct recipeProduct)
        {
            ctx.RecipeProducts.Remove(recipeProduct);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }
    }
}
