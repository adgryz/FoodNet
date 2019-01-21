using FoodNet.DataAccessCore;
using FoodNet.ModelCore.DTO;
using FoodNET.WebAPI.Data.DTOFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FoodNET.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [Authorize]
    public class ResultsController : Controller
    {
        private FoodNetDbContext _context;
        private ResultDataDTOFacade _resultDataDTOFacade;

        public ResultsController(FoodNetDbContext context, ResultDataDTOFacade resultDataDTOFacade)
        {
            _context = context;
            _resultDataDTOFacade = resultDataDTOFacade;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst("sub").Value;
            return Guid.Parse(userId);
        }

        [HttpGet("all")]
        public IEnumerable<RecipeDTO> GetAllMatchingRecipes(List<Guid> tags)
        {
            var userId = GetUserId();
            return _resultDataDTOFacade.GetAllMatchingPublicPlusUserRecipes(userId, tags);
        }

        [HttpGet("")]
        public IEnumerable<RecipeDTO> GetAllUserMatchingRecipes(List<Guid> tags)
        {
            var userId = GetUserId();
            return _resultDataDTOFacade.GetAllUserMatchingRecipes(userId, tags);
        }
    }
}
