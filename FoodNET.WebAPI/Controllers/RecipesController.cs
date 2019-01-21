using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNet.ModelCore.DTO;
using FoodNET.WebAPI.Data.DTOFacade;
using FoodNET.WebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodNET.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [Authorize]
    public class RecipesController : Controller
    {
        private FoodNetDbContext _context;
        private RecipesDTOFacade _recipesDTOFacade;
        private IRecipesRepository _recipesRepository;
        private readonly UserManager<User> _userManager;

        public RecipesController(FoodNetDbContext context,IRecipesRepository recipesRepository,
            RecipesDTOFacade recipesDTOFacade, UserManager<User> userManager)
        {
            _context = context;
            _recipesDTOFacade = recipesDTOFacade;
            _recipesRepository = recipesRepository;
            _userManager = userManager;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst("sub").Value;
            return Guid.Parse(userId);
        }
        [HttpGet("all")]
        public IEnumerable<RecipeDTO> GetAllPublicPlusUserRecipes(List<Guid> tags)
        {
            var userId = GetUserId();
            return _recipesDTOFacade.GetAllPublicPlusUserRecipes(userId, tags);
        }

        [HttpGet("")]
        public IEnumerable<RecipeDTO> GetAllUserRecipes(List<Guid> tags)
        {
            var userId = GetUserId();
            return _recipesDTOFacade.GetAllUserRecipes(userId, tags);
        }

        [HttpPost("")]
        public IActionResult UpdateRecipe([FromBody] BlankRecipeUpdateDTO updatedRecipe)
        {
            RecipeUpdateDTO updatedRecipeWithUser = new RecipeUpdateDTO
            {
                Id = updatedRecipe.Id,
                Title = updatedRecipe.Title,
                Description = updatedRecipe.Description,
                Products = updatedRecipe.Products,
                Tags = updatedRecipe.Tags,
                IsPrivate = updatedRecipe.IsPrivate,
                UserId = GetUserId(),
            };

            _recipesDTOFacade.UpdateRecipe(updatedRecipeWithUser);

            return Json(true);
        }

        [HttpPut("")]
        public IActionResult AddRecipe([FromBody] BlankRecipeUpdateDTO newRecipe)
        {
            RecipeUpdateDTO newRecipeWithUser = new RecipeUpdateDTO
            {
                Id = newRecipe.Id,
                Title = newRecipe.Title,
                Description = newRecipe.Description,
                Products = newRecipe.Products,
                Tags = newRecipe.Tags,
                IsPrivate = newRecipe.IsPrivate,
                UserId = GetUserId(),
            };
            _recipesDTOFacade.AddRecipe(newRecipeWithUser);

	    return Json(true);
        }

        [HttpDelete("")]
        public IActionResult RemoveRecipe([FromBody]Guid id)
        {
            var recipeToDel = _recipesRepository.GetRecipeById(id);
            if (recipeToDel == null || recipeToDel.UserId != GetUserId())
                return Json(false);
            _recipesRepository.RemoveRecipe(id);
            return Json(true);
        }
    }
}
