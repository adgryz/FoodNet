using FoodNet.DataAccess;
using FoodNet.Model.Domain;
using FoodNet.UI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodNet.UI.Data
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
