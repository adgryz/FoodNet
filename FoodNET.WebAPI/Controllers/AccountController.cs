using FoodNet.ModelCore.Accounts;
using FoodNet.ModelCore.Domain;
using FoodNET.WebAPI.Data.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodNET.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFridgeRepository _fridgeRepository;
        public AccountController(UserManager<User> userManager, IFridgeRepository fridgeRepository)
        {
            _fridgeRepository = fridgeRepository;
            _userManager = userManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> RegisterUsr([FromBody] SignUpModel model)
        {
            var id = Guid.NewGuid();
            User user = new User
            {
                Id = id,
                Email = model.Email,
                Name = model.Name,
                UserName = model.Name
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, model.Email));
            await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, "user"));
            _fridgeRepository.CreateFridgeForUser(id);
            return Json(true);
        }

        [HttpPost("SignOut")]
        public async void SignOut()
        {
            await HttpContext.SignOutAsync();
        }
    }
}
