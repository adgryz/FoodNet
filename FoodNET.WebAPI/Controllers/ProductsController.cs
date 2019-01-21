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
    public class ProductsController : Controller
    {

        private FoodNetDbContext _context;
        private ProductsDTOFacade _productsDTOFacade;

        public ProductsController(FoodNetDbContext context, ProductsDTOFacade productsDTOFacade)
        {
            _context = context;
            _productsDTOFacade = productsDTOFacade;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirst("sub").Value;
            return Guid.Parse(userId);
        }

        [HttpGet("")]
        public IEnumerable<ProductDTO> GetAllProductsForUser()
        {
            var UserId = GetUserId();
            return _productsDTOFacade.GetAllProductsForUser(UserId);
        }

        // temp
        [HttpGet("basic")]
        public IEnumerable<ProductDTO> GetAllBasicProducts()
        {
            return _productsDTOFacade.GetAllBasicProducts();
        }

        // temp
        [HttpGet("new")]
        public IEnumerable<ProductDTO> GetAllNewProducts()
        {
            return _productsDTOFacade.GetAllNewProducts();
        }

        [HttpGet("categories/{categoryId}")]
        public IEnumerable<ProductDTO> GetAllProductsForCategory(Guid categoryId)
        {
            return _productsDTOFacade.GetAllProductsForCategory(categoryId);
        }

        [HttpGet("categories")]
        public IEnumerable<ProductCategoryDTO> GetAllCategories()
        {
            return _productsDTOFacade.GetAllCategories();
        }

        [HttpPut("")]
        public IActionResult AddProduct([FromBody] ProductDTO newProduct)
        {
            _productsDTOFacade.AddNewProduct(newProduct, GetUserId());
            return Json(true);
        }

    }
}
