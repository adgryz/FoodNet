using FoodNet.DataAccess;
using FoodNet.Model.Domain;
using FoodNet.UI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FoodNet.UI.Data
{
    public class ResultDataService : IResultDataService
    {
        private Func<FoodNetDbContext> _dbContextCreator;

        public ResultDataService(Func<FoodNetDbContext> dbContextCreator)
        {
            _dbContextCreator = dbContextCreator;
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            using (var ctx = _dbContextCreator())
            {
                var exceptionList = ctx.RecipeProducts
                    .GroupJoin(ctx.FridgeProducts,
                        rp => rp.ProductId,
                        fp => fp.ProductId,
                        (rp, fp) => new { FridgeProduct = fp, RecipeProduct = rp })
                    .SelectMany(p => p.FridgeProduct.DefaultIfEmpty(), (x, y) =>
                               new { FridgeProduct = y, RecipeProduct = x.RecipeProduct })// RIGHT JOIN 
                   .Where(p => p.FridgeProduct == null)
                   .Select(p => p.RecipeProduct.Recipe.Id).
                   Distinct().ToList();

                var result = ctx.Recipes.Include(r => r.RecipeProducts).Where(r => !exceptionList.Contains(r.Id)).ToList();
                return result;
            }
        }

    }
}
