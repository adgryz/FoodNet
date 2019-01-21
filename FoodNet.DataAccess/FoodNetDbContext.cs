using FoodNet.Model.Domain;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
namespace FoodNet.DataAccess
{
    public class FoodNetDbContext: DbContext
    {
        public FoodNetDbContext():base("FoodNetDb")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<FridgeProduct> FridgeProducts { get; set; }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeProduct> RecipeProducts { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
