using FoodNet.ModelCore.Domain;

namespace FoodNET.WebAPI.Data.Interfaces
{
    public interface IUsersRepository
    {
        User GetFirstUser();
    }
}