using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNET.WebAPI.Data.Interfaces;
using System.Linq;

namespace FoodNET.WebAPI.Data
{
    public class UsersRepository : IUsersRepository
    {
        private FoodNetDbContext _context;

        public UsersRepository(FoodNetDbContext context)
        {
            _context = context;
        }
        public User GetFirstUser()
        {
            return _context.Users.First();
        }
    }
}
