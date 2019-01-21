using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNET.WebAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodNET.WebAPI.Data
{
    public class ResultDataService : IResultDataService
    {
        private FoodNetDbContext _context;
        private IFridgeRepository _fridgeRepository;


        public ResultDataService(FoodNetDbContext context, IFridgeRepository fridgeRepository)
        {
            _fridgeRepository = fridgeRepository;
            _context = context;
        }

        public IEnumerable<Recipe> GetAllMatchingPublicPlusUserRecipes(Guid userId, List<Guid> tags = null)
        {
            tags = tags ?? new List<Guid>();

            var fridgeId = _fridgeRepository.GetUserFridge(userId).Id;
                var exceptionList = _context.RecipeProducts
                    .GroupJoin(_context.FridgeProducts.Where(fp => fp.FridgeId == fridgeId).ToList(),
                        rp => rp.ProductId,
                        fp => fp.ProductId,
                        (rp, fp) => new { FridgeProduct = fp, RecipeProduct = rp })
                    .SelectMany(p => p.FridgeProduct.DefaultIfEmpty(), (x, y) =>
                               new { FridgeProduct = y, RecipeProduct = x.RecipeProduct })// RIGHT JOIN 
                   .Where(p => p.FridgeProduct == null)
                   .Select(p => p.RecipeProduct.Recipe.Id).
                   Distinct().ToList();

            var result = _context.Recipes
                .Include(r => r.RecipeProducts)
                .Include(r => r.User)
                .Include(r => r.RecipeTags)
                    .ThenInclude(rt => rt.Tag)
                .Where(r => !exceptionList.Contains(r.Id))
                .Where(r => r.UserId == userId || !r.IsPrivate)
                .ToList();

            if (tags.Count() == 0)
            {
                return result;
            }

            var tagsCompliant = _context.RecipeTags
            .GroupBy(rt => rt.RecipeId,
                     rt => rt.TagId,
                     (recipeId, recipeTags) => new { RecipeId = recipeId, Tags = recipeTags.ToList() })
            .Where(x => !tags.Except(x.Tags).Any())
            .Select(r => r.RecipeId)
            .ToList();

            return result.Where(r => tagsCompliant.Contains(r.Id));
        }

        public IEnumerable<Recipe> GetMatchingUserRecipes(Guid userId, List<Guid> tags = null)
        {
            return GetAllMatchingPublicPlusUserRecipes(userId, tags)
                .Where(r => r.UserId == userId).ToList();
        }
    }
}
