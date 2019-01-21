using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using FoodNet.UI.Data.Interfaces;
using FoodNet.UI.Event;
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
    public class RecipesViewModel : ViewModelBase
    {
        private IRecipesRepository _recipesRepository;
        private IProductsRepository _productsRepository;
        private IUsersRepository _usersRepository;
        private IEventAggregator _eventAggregator;

        public RecipesViewModel(IRecipesRepository recipesRepository,
                                    IProductsRepository productsRepository,
                                    IUsersRepository usersRepository,
                                    IEventAggregator eventAggregator)
        {
            _recipesRepository = recipesRepository;
            _productsRepository = productsRepository;
            _usersRepository = usersRepository;
            _eventAggregator = eventAggregator;
            EditMode = false;

            LoadDataCommand = new DelegateCommand(OnLoadDataExecute);

            OpenNewProductDialogCommand = new DelegateCommand(OnOpenNewProductDialogExecute);
            AddCommand = new DelegateCommand(OnAddCommandExecute, CanAddCommandExecute);
            CancelCommand = new DelegateCommand(OnCancelCommandExecute);

            ToggleEditMode = new DelegateCommand(OnToggleEditModeExecute);
            SaveChanges = new DelegateCommand(OnSaveChanges);
            DiscardChanges = new DelegateCommand(OnDiscardChanges);
            RemoveRecipeProduct = new DelegateCommand(OnRemoveRecipeProduct);
            AddRecipeProduct = new DelegateCommand(OnAddRecipeProduct);
            AddRecipe = new DelegateCommand(OnAddRecipe);
            RemoveRecipe = new DelegateCommand(OnRemoveRecipe);

            _eventAggregator.GetEvent<NewProductAddedEvent>().Subscribe(AfterNewProductAdded);
            _eventAggregator.GetEvent<NewRecipeAddedEvent>().Subscribe(AfterNewRecipeAdded);
            _eventAggregator.GetEvent<RecipeRemovedEvent>().Subscribe(AfterRecipeDeleted);
            _eventAggregator.GetEvent<RecipeUpdatedEvent>().Subscribe(AfterRecipeModified);
        }

        public ICommand LoadDataCommand { get; }
        protected virtual void OnLoadDataExecute()
        {
            Recipes = new ObservableCollection<Recipe>(_recipesRepository.GetAllRecipes());
            Products = new ObservableCollection<Product>(_productsRepository.GetAllProducts());
            Categories = new ObservableCollection<ProductCategory>(_productsRepository.GetAllCategories());
        }

        private void AfterNewProductAdded(NewProductAddedEventArgs obj)
        {
            Product newProduct = obj.NewProduct;
            if (Products != null)
                Products.Add(newProduct);
        }

        private void AfterNewRecipeAdded(NewRecipeAddedEventArgs obj)
        {
            Recipe newRecipe = obj.NewRecipe;
            if (Recipes != null && !Recipes.Contains(newRecipe))
                Recipes.Add(newRecipe);
        }

        private void AfterRecipeDeleted(RecipeRemovedEventArgs obj)
        {
            Recipe removedRecipe = obj.RemovedRecipe;
            if (Recipes != null)
                Recipes.Remove(removedRecipe);
        }

        private void AfterRecipeModified(RecipeUpdatedEventArgs obj)
        {
            Recipe modifiedRecipe = obj.UpdatedRecipe;
            if (Recipes != null)
            {
                Recipe unmodifiedRecipe = Recipes.Where(r => r.Id == modifiedRecipe.Id).FirstOrDefault();
                if (unmodifiedRecipe == null)
                {
                    //selected recipe isn't in our set, we're in result view
                    return;
                }
                int index = Recipes.IndexOf(unmodifiedRecipe);
                Recipes[index] = null;
                Recipes[index] = modifiedRecipe;
            }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Recipe> _recipes;
        public ObservableCollection<Recipe> Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                OnPropertyChanged();
            }
        }

        private void LoadSelectedRecipeProductLookups()
        {
            if (SelectedRecipe != null)
            {
                SelectedRecipeProductLookups = new ObservableCollection<RecipeProductLookup>(_recipesRepository
                .GetRecipeProductLookupsForRecipe(SelectedRecipe.Id));
            } else
            {
                SelectedRecipeProductLookups = null;
            }
        }

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                if (value == null)
                {
                    EditMode = false;
                    UnchangedSelectedRecipe = null;
                }
                _selectedRecipe = value;
                OnPropertyChanged();
                LoadSelectedRecipeProductLookups();
            }
        }

        private Recipe _unchangedSelectedRecipe;
        public Recipe UnchangedSelectedRecipe
        {
            get { return _unchangedSelectedRecipe; }
            set
            {
                _unchangedSelectedRecipe = value;
            }
        }

        private ObservableCollection<RecipeProductLookup> _selectedRecipeProductLookups;
        public ObservableCollection<RecipeProductLookup> SelectedRecipeProductLookups
        {
            get { return _selectedRecipeProductLookups; }
            set
            {
                _selectedRecipeProductLookups = value;
                OnPropertyChanged();
            }
        }

        private RecipeProductLookup _selectedRecipeProductLookup;
        public RecipeProductLookup SelectedRecipeProductLookup
        {
            get { return _selectedRecipeProductLookup; }
            set
            {
                _selectedRecipeProductLookup = value;
                if (value != null)
                {
                    RecipeProductLookupSelected = true;
                }
                else
                {
                    RecipeProductLookupSelected = false;
                }
                OnPropertyChanged();
            }
        }

        private Boolean _recipeProductLookupSelected;
        public Boolean RecipeProductLookupSelected
        {
            get { return _recipeProductLookupSelected; }
            set
            {
                _recipeProductLookupSelected = value;
                OnPropertyChanged();
            }
        }

        private Product _selectedSearchBoxProduct;
        public Product SelectedSearchBoxProduct
        {
            get { return _selectedSearchBoxProduct; }
            set
            {
                _selectedSearchBoxProduct = value;
                OnPropertyChanged();
            }
        }

        private void ClearSelectedProductUserInput()
        {
            SelectedSearchBoxProduct = new Product
            {
                Name = string.Empty,
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };
        }

        private Boolean _editMode;
        public Boolean EditMode
        {
            get { return _editMode; }
            set
            {
                if (SelectedRecipe != null)
                {
                    _editMode = value;
                    UnchangedSelectedRecipe = SelectedRecipe.DeepCopy();
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveChanges { get; }
        private void OnSaveChanges()
        {
            List<RecipeProduct> recipeProductsToRemove = new List<RecipeProduct>();
            List<RecipeProduct> recipeProductsToAdd = new List<RecipeProduct>();

            //remove products that were present only before changes 
            UnchangedSelectedRecipe.RecipeProducts.ForEach(rp =>
            {
                if (!SelectedRecipe.RecipeProducts.Where(rp2 => rp2.Id == rp.Id).Any())
                {
                    recipeProductsToRemove.Add(rp);
                }
            });
            //add products that weren't present before
            SelectedRecipe.RecipeProducts.ForEach(rp =>
            {
                if (!UnchangedSelectedRecipe.RecipeProducts.Where(rp2 => rp2.Id == rp.Id).Any())
                {
                    recipeProductsToAdd.Add(rp);
                }
            });

            recipeProductsToAdd.ForEach(rp => _recipesRepository.AddRecipeProduct(rp));
            recipeProductsToRemove.ForEach(rp => _recipesRepository.RemoveRecipeProduct(rp));
            //_recipesRepository.UpdateRecipe(SelectedRecipe);
            _recipesRepository.Save();
            EditMode = false;

            ClearSelectedProductUserInput();

            _eventAggregator.GetEvent<RecipeUpdatedEvent>().Publish(
                    new RecipeUpdatedEventArgs { UpdatedRecipe = SelectedRecipe }
                );
        }

        public ICommand DiscardChanges { get; }
        private void OnDiscardChanges()
        {
            if (SelectedRecipe != null)
            {
                SelectedRecipe.Title = UnchangedSelectedRecipe.Title;
                SelectedRecipe.Description = UnchangedSelectedRecipe.Description;
                SelectedRecipe.RecipeProducts = new List<RecipeProduct>(UnchangedSelectedRecipe.RecipeProducts);

                _eventAggregator.GetEvent<RecipeUpdatedEvent>().Publish(
                     new RecipeUpdatedEventArgs { UpdatedRecipe = SelectedRecipe }
                );
            }
            EditMode = false;
            SelectedRecipe = null;
            SelectedRecipeProductLookups = null;

            ClearSelectedProductUserInput();
        }

        public ICommand ToggleEditMode { get; }
        private void OnToggleEditModeExecute()
        {
            EditMode = !EditMode;
            OnPropertyChanged();
        }

        private void OnRemoveRecipeProduct()
        {
            if (SelectedRecipe != null && SelectedRecipeProductLookup != null)
            {
                SelectedRecipe.RecipeProducts.Remove(
                    SelectedRecipe.RecipeProducts.Where(rp => rp.Id == SelectedRecipeProductLookup.RecipeProductId).First());
                SelectedRecipeProductLookups.Remove(SelectedRecipeProductLookups
                    .Where(srpl => srpl.ProductId == SelectedRecipeProductLookup.ProductId).First());

                RecipeProductLookupSelected = false;
            }

        }
        public ICommand RemoveRecipeProduct { get; }

        private void OnAddRecipeProduct()
        {
            if (SelectedRecipe != null && SelectedSearchBoxProduct != null
                && !SelectedRecipeProductLookups.Where(srpl => srpl.ProductId == SelectedSearchBoxProduct.Id).Any())
            {
                if(SelectedSearchBoxProduct.Id.Equals(Guid.Parse("00000000-0000-0000-0000-000000000000"))) {
                    return;
                }
                RecipeProduct recipeProduct = new RecipeProduct
                {
                    Id = Guid.NewGuid(),
                    RecipeId = SelectedRecipe.Id,
                    ProductId = SelectedSearchBoxProduct.Id
                };
                SelectedRecipe.RecipeProducts.Add(recipeProduct);
                SelectedRecipeProductLookups.Add(new RecipeProductLookup
                {
                    ProductId = SelectedSearchBoxProduct.Id,
                    ProductName = SelectedSearchBoxProduct.Name,
                    RecipeProductId = recipeProduct.Id
                });
                OnPropertyChanged();
            }
            ClearSelectedProductUserInput();
        }
        public ICommand AddRecipeProduct { get; }

        private void OnAddRecipe()
        {
            Recipe recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                UserId = _usersRepository.GetFirstUser().Id,
                Title = "<add title>",
                Description = "<add description>",
                RecipeProducts = new List<RecipeProduct>()
            };
            _recipesRepository.AddRecipe(recipe);
            _recipesRepository.Save();
            SelectedRecipe = recipe;
            Recipes.Add(recipe);
            EditMode = true;

            _eventAggregator.GetEvent<NewRecipeAddedEvent>().Publish(
                    new NewRecipeAddedEventArgs { NewRecipe = recipe }
                );
        }
        public ICommand AddRecipe { get; }

        private void OnRemoveRecipe()
        {
            if (SelectedRecipe != null)
            {
                _recipesRepository.RemoveRecipe(SelectedRecipe);
                Recipes.Remove(SelectedRecipe);
                _recipesRepository.Save();

                _eventAggregator.GetEvent<RecipeRemovedEvent>().Publish(
                        new RecipeRemovedEventArgs { RemovedRecipe = SelectedRecipe }
                    );
            }
        }
        public ICommand RemoveRecipe { get; }

        // NewProductDialog
        private bool _isDialogOpen;
        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set
            {
                _isDialogOpen = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenNewProductDialogCommand { get; }
        private void OnOpenNewProductDialogExecute()
        {
            if (SelectedRecipe != null)
            {
                IsDialogOpen = true;
                NewProductName = "";
                NewProductDescription = "";
                NewProductCategory = null;
                newProduct = null;
            }
        }

        private ObservableCollection<ProductCategory> _categories;
        public ObservableCollection<ProductCategory> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public Product newProduct { get; set; }
        private string _newProductName;
        public string NewProductName
        {
            get { return _newProductName; }
            set
            {
                _newProductName = value;
                OnPropertyChanged();
                ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }
        private string _newProductDescription;
        public string NewProductDescription
        {
            get { return _newProductDescription; }
            set
            {
                _newProductDescription = value;
                OnPropertyChanged();
            }
        }
        private ProductCategory _newProductCategory;
        public ProductCategory NewProductCategory
        {
            get { return _newProductCategory; }
            set
            {
                _newProductCategory = value;
                OnPropertyChanged();
                ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand CancelCommand { get; }
        private void OnCancelCommandExecute()
        {
            IsDialogOpen = false;
        }

        public ICommand AddCommand { get; }
        private void OnAddCommandExecute()
        {
            IsDialogOpen = false;
            newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = NewProductName,
                Description = NewProductDescription,
                ProductCategory = NewProductCategory
            };

            Products.Add(newProduct);
            _productsRepository.AddProduct(newProduct);
            _productsRepository.Save();
            RecipeProduct recipeProduct = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                RecipeId = SelectedRecipe.Id,
                ProductId = newProduct.Id
            };
            SelectedRecipe.RecipeProducts.Add(recipeProduct);
            SelectedRecipeProductLookups.Add(new RecipeProductLookup
            {
                ProductId = newProduct.Id,
                RecipeProductId = recipeProduct.Id,
                ProductName = newProduct.Name
            });

            _eventAggregator.GetEvent<NewProductAddedEvent>().Publish(
                new NewProductAddedEventArgs
                {
                    NewProduct = newProduct
                });

        }
        private bool CanAddCommandExecute()
        {
            return !String.IsNullOrWhiteSpace(NewProductName) && NewProductCategory != null;
        }
    }
}
