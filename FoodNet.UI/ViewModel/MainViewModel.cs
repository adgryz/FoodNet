using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace FoodNet.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private FridgeViewModel _fridgeViewModel;
        private RecipesBookViewModel _recipesBookViewModel;
        private StartViewModel _startViewModel;
        private ResultViewModel _resultViewModel;
        private RecipeControlViewModel _recipeControlViewModel;

        public MainViewModel(FridgeViewModel fridgeViewModel, RecipesBookViewModel recipesBookViewModel,
            StartViewModel startViewModel, ResultViewModel resultViewModel, RecipeControlViewModel recipeControlViewModel)
        {
            _fridgeViewModel = fridgeViewModel;
            _recipesBookViewModel = recipesBookViewModel;
            _startViewModel = startViewModel;
            _resultViewModel = resultViewModel;
            _recipeControlViewModel = recipeControlViewModel;

            CurrentViewModel = _startViewModel;

            DisplayRecipesBookView = new DelegateCommand(OnDisplayRecipesBookViewExecute);
            DisplayFridgeView = new DelegateCommand(OnDisplayFridgeViewExecute);
            DisplayResultView = new DelegateCommand(OnDisplayResultViewExecute);
            DisplayRecipeControl = new DelegateCommand(OnDisplayRecipeControlExecute);
        }

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand DisplayRecipeControl { get; }
        private void OnDisplayRecipeControlExecute()
        {
            CurrentViewModel = _recipeControlViewModel;
        }

        public ICommand DisplayRecipesBookView { get;  }
        private void OnDisplayRecipesBookViewExecute()
        {
            CurrentViewModel = _recipesBookViewModel;
        }

        public ICommand DisplayFridgeView { get; }
        private void OnDisplayFridgeViewExecute()
        {
            CurrentViewModel = _fridgeViewModel;
        }

        public ICommand DisplayResultView { get; }
        private void OnDisplayResultViewExecute()
        {
            CurrentViewModel = _resultViewModel;
        }
    }
}
