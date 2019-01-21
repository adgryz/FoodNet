using System.Collections.Generic;
using FoodNet.Model.Domain;

namespace FoodNet.UI.Data.Interfaces
{
    public interface IResultDataService
    {
        IEnumerable<Recipe> GetRecipes();
    }
}