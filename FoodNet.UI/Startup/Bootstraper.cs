using Autofac;
using FoodNet.DataAccess;
using FoodNet.UI.Data;
using FoodNet.UI.Data.Interfaces;
using FoodNet.UI.ViewModel;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FoodNet.UI.Startup
{
    public class Bootstraper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FoodNetDbContext>().AsSelf();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<FridgeRepository>().As<IFridgeRepository>();
            builder.RegisterType<ProductsRepository>().As<IProductsRepository>();
            builder.RegisterType<RecipesRepository>().As<IRecipesRepository>();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            builder.RegisterType<ResultDataService>().As<IResultDataService>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<ResultViewModel>().AsSelf();
            builder.RegisterType<StartViewModel>().AsSelf();
            builder.RegisterType<FridgeViewModel>().AsSelf(); 
            builder.RegisterType<RecipesBookViewModel>().AsSelf();
            builder.RegisterType<RecipeControlViewModel>().AsSelf();

            return builder.Build();
        }
    }
}
