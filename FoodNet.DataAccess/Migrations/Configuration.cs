namespace FoodNet.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodNet.DataAccess.FoodNetDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FoodNet.DataAccess.FoodNetDbContext context)
        {

        }
    }
}
