using FoodNet.DataAccessCore;
using FoodNet.ModelCore.DTO;
using FoodNET.WebAPI.Data.DTOFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNET.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [Authorize]
    public class TagsController : Controller
    {
        private FoodNetDbContext _context;
        private TagsDTOFacade _tagsDTOFacade;

        public TagsController(FoodNetDbContext context, TagsDTOFacade tagsDTOFacade)
        {
            _context = context;
            _tagsDTOFacade = tagsDTOFacade;
        }

        [HttpGet("all")]
        public IEnumerable<TagDTO> GetAllTags()
        {
            return _tagsDTOFacade.GetAllTags();
        }
    }
}
