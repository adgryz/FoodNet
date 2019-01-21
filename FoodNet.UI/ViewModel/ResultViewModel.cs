using FoodNet.Model.Domain;
using FoodNet.UI.Data.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.ObjectModel;

namespace FoodNet.UI.ViewModel
{
    public class ResultViewModel : RecipesViewModel
    {
        private IResultDataService _resultDataService;

        public ResultViewModel(IResultDataService resultDataService,
                                    IRecipesRepository recipesRepository,
                                    IProductsRepository productsRepository,
                                    IUsersRepository usersRepository,
                                    IEventAggregator eventAggregator) : base(recipesRepository, productsRepository, usersRepository, eventAggregator)
        {
            _resultDataService = resultDataService;
        }

        protected override void OnLoadDataExecute()
        {
            base.OnLoadDataExecute();
            Recipes = new ObservableCollection<Recipe>(_resultDataService.GetRecipes());
        }
    }
}
