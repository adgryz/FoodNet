using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNet.ModelCore.Lookups;
using FoodNET.WebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodNET.WebAPI.Data
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
            List<Product> productsForRecipe = new List<Product>();
            productsForRecipe.AddRange(ctx.RecipeProducts.Where(rp => rp.RecipeId == recipeId)
                 .Join(ctx.BasicProducts, rp => rp.ProductId, p => p.Id, (rp, p) => p)
                 .ToList());
            productsForRecipe.AddRange(ctx.RecipeProducts.Where(rp => rp.RecipeId == recipeId)
                .Join(ctx.NewProducts, rp => rp.ProductId, p => p.Id, (rp, p) => p)
                .ToList());
            return productsForRecipe;
        }

        public bool ContainsProduct(Guid recipeId, Guid productId)
        {
            return ctx.RecipeProducts.Where(rp => rp.ProductId == productId && rp.RecipeId == recipeId).Any();
        }

        public IEnumerable<Recipe> GetAllPublicPlusUserRecipes(Guid userId, List<Guid> tags = null)
        {
            tags = tags ?? new List<Guid>();

            if(tags.Count() == 0)
            {
                return ctx.Recipes.Where(r => r.UserId == userId || !r.IsPrivate)
                    .Include(r => r.RecipeProducts)
                    .Include(r => r.User)
                    .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                    .ToList();
            }
            else
            {
                return ctx.RecipeTags
                .GroupBy(rt => rt.RecipeId,
                         rt => rt.TagId,
                         (recipeId, recipeTags) => new { RecipeId = recipeId, Tags = recipeTags.ToList() })
                .Where(x => !tags.Except(x.Tags).Any())
                .Join(ctx.Recipes,
                      x => x.RecipeId,
                      r => r.Id,
                      (x, r) => r)
                .Where(r => r.UserId == userId || !r.IsPrivate)
                .Include(r => r.RecipeProducts)
                .Include(r => r.User)
                .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                .ToList();
            }
        }

        public IEnumerable<Recipe> GetAllUserRecipes(Guid userId, List<Guid> tags = null)
        {
            tags = tags ?? new List<Guid>();

            if (tags.Count() == 0)
            {
                return ctx.Recipes.Where(r => r.UserId == userId)
                    .Include(r => r.RecipeProducts)
                    .Include(r => r.User)
                    .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                    .ToList();
            }
            else
            {
                return ctx.RecipeTags
                .GroupBy(rt => rt.RecipeId,
                         rt => rt.TagId,
                         (recipeId, recipeTags) => new { RecipeId = recipeId, Tags = recipeTags.ToList() })
                .Where(x => !tags.Except(x.Tags).Any())
                .Join(ctx.Recipes,
                      x => x.RecipeId,
                      r => r.Id,
                      (x, r) => r)
                .Where(r => r.UserId == userId)
                .Include(r => r.RecipeProducts)
                .Include(r => r.User)
                .Include(r => r.RecipeTags)
                        .ThenInclude(rt => rt.Tag)
                .ToList();
            }
        }

        public IEnumerable<RecipeProductLookup> GetRecipeProductLookupsForRecipe(Guid recipeId)
        {
            List<RecipeProductLookup> recipeProductLookups = new List<RecipeProductLookup>();
            recipeProductLookups.AddRange((from rp in ctx.RecipeProducts
                          join bp in ctx.BasicProducts
                          on rp.ProductId equals bp.Id
                          where rp.RecipeId == recipeId
                          select new RecipeProductLookup
                          {
                              ProductId = bp.Id,
                              ProductName = bp.Name,
                              RecipeProductId = rp.Id
                          }).ToList());
            recipeProductLookups.AddRange((from rp in ctx.RecipeProducts
                          join np in ctx.NewProducts
                          on rp.ProductId equals np.Id
                          where rp.RecipeId == recipeId
                          select new RecipeProductLookup
                          {
                              ProductId = np.Id,
                              ProductName = np.Name,
                              RecipeProductId = rp.Id
                          }).ToList());
            return recipeProductLookups;
        }

        public void UpdateRecipeSimpleValues(Recipe recipe)
        {
            var entry = ctx.Recipes.Where(r => r.Id == recipe.Id).Single();
            entry.Title = recipe.Title;
            entry.Description = recipe.Description;
            entry.IsPrivate = recipe.IsPrivate;
            Save();
        }
        public void AddProduct(RecipeProduct recipeProduct)
        {
            ctx.RecipeProducts.Add(recipeProduct);
            Save();
        }
        public void RemoveRecipeProduct(RecipeProduct recipeProduct)
        {
            ctx.RecipeProducts.Remove(recipeProduct);
            Save();
        }

        public void AddTag(RecipeTag recipeTag)
        {
            ctx.RecipeTags.Add(recipeTag);
            Save();
        }
        public void RemoveRecipeTag(RecipeTag recipeTag)
        {
            ctx.RecipeTags.Remove(recipeTag);
            Save();
        }
        public void RemoveTagById(Guid tagId)
        {
            var recipeTag = ctx.RecipeTags.Where(rt => rt.TagId == tagId).FirstOrDefault();
            if (recipeTag != null)
            {
                ctx.RecipeTags.Remove(recipeTag);
                Save();
            }
        }

        public bool ContainsTag(Guid recipeId, Guid tagId)
        {
            return ctx.RecipeTags.Where(rt => rt.TagId == tagId && rt.RecipeId == recipeId).Any();
        }

        public void RemoveRecipe(Guid id)
        {
            Recipe delRecipe = GetRecipeById(id);
            if (delRecipe != null)
            {
                ctx.Recipes.Remove(delRecipe);
                Save();
            }
        }

        public void AddRecipe(Recipe recipe)
        {
            ctx.Recipes.Add(recipe);
            Save();
        }

        public void RemoveProductById(Guid productId)
        {
            var recipeProduct = ctx.RecipeProducts.Where(rp => rp.ProductId == productId).FirstOrDefault();
            if (recipeProduct != null)
            {
                ctx.RecipeProducts.Remove(recipeProduct);
                Save();
            }
        }

        public Recipe GetRecipeById(Guid recipeId)
        {
            return ctx.Recipes.Where(r => r.Id == recipeId)
                .Include(r => r.RecipeProducts)
                .Include(r => r.RecipeTags)
                .FirstOrDefault();
        }

        private void Save()
        {
            ctx.SaveChanges();
        }

        private RecipeProduct GetRecipeProductById(Guid recipeProductId)
        {
            return ctx.RecipeProducts.Where(rp => rp.Id == recipeProductId).Single();
        }
    }
}
