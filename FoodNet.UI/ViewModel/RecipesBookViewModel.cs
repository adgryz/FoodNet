using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using FoodNet.UI.Data.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace FoodNet.UI.ViewModel
{
    public class RecipesBookViewModel : RecipesViewModel
    {

        public RecipesBookViewModel(IRecipesRepository recipesRepository,
                                    IProductsRepository productsRepository,
                                    IUsersRepository usersRepository,
                                    IEventAggregator eventAggregator) : base( recipesRepository, productsRepository, usersRepository, eventAggregator)
        {
        }
    }
}
