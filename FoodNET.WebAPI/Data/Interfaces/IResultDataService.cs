using System.Collections.Generic;
using FoodNet.ModelCore.Domain;
using System;

namespace FoodNET.WebAPI.Data.Interfaces
{
    public interface IResultDataService
    {
        IEnumerable<Recipe> GetAllMatchingPublicPlusUserRecipes(Guid userId, List<Guid> tags);
        IEnumerable<Recipe> GetMatchingUserRecipes(Guid userId, List<Guid> tags);
    }
}