using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNet.ModelCore.DTO;
using FoodNET.WebAPI.Data.DTOFacade;
using FoodNET.WebAPI.Data.Interfaces;
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
    public class FridgeController : Controller
    {
        private FoodNetDbContext _context;
        private IFridgeRepository _fridgeRepository;
        private FridgesDTOFacade _fridgesDTOFacade;
        private IPriorityUserProductsRepository _priorityUserProductsRepository;

        public FridgeController(FoodNetDbContext context,
            IFridgeRepository fridgeRepository, 
            FridgesDTOFacade fridgesDTOFacade,
            IPriorityUserProductsRepository priorityUserProductsRepository)
        {
            _context = context;
            _fridgeRepository = fridgeRepository;
            _fridgesDTOFacade = fridgesDTOFacade;
            _priorityUserProductsRepository = priorityUserProductsRepository;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst("sub").Value;
            return Guid.Parse(userId);
        }

        [HttpGet("")]
        public Fridge GetUserFridge()
        {
            var UserId = GetUserId();
            return _fridgeRepository.GetUserFridge(UserId);
        }

        [HttpGet("products")]
        public IEnumerable<ProductDTO> GetAllFridgeProducts()
        {
            var userId = GetUserId();
            return _fridgesDTOFacade.GetAllFridgeProducts(userId);
        }

        [HttpDelete("")]
        public IActionResult DeleteProduct([FromBody] Guid productId)
        {
            _fridgeRepository.DeleteProduct(productId);
            return Json(true);
        }

        [HttpPut("")]
        public IActionResult AddProduct([FromBody] Guid productId)
        {
            _fridgesDTOFacade.AddProduct(GetUserId(), productId);
            _priorityUserProductsRepository.IncrementPriorityCount(GetUserId(), productId);
            return Json(true);
        }
    }
}
