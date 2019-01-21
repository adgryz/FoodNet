using FoodNet.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodNet.UI.Data.Interfaces
{
    public interface IUsersRepository
    {
        User GetFirstUser();
    }
}
