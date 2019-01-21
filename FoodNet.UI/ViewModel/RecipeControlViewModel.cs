using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using FoodNet.UI.Data.Interfaces;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace FoodNet.UI.ViewModel
{
    public class RecipeControlViewModel : ViewModelBase
    {
        private IRecipesRepository _recipesRepository;
        private IProductsRepository _productsRepository;
        private IUsersRepository _usersRepository;

        public RecipeControlViewModel(IRecipesRepository recipesRepository,
                                    IProductsRepository productsRepository,
                                    IUsersRepository usersRepository)
        {
            _recipesRepository = recipesRepository;
            _productsRepository = productsRepository;
            _usersRepository = usersRepository;
            EditMode = false;

            Recipes = new ObservableCollection<Recipe>(_recipesRepository.GetAllRecipes());
            Products = new ObservableCollection<Product>(_productsRepository.GetAllProducts());
            Categories = new ObservableCollection<ProductCategory>(_productsRepository.GetAllCategories());

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
        }

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Recipe> Recipes { get; set; }

        private void LoadSelectedRecipeProductLookups()
        {
            if (SelectedRecipe != null)
            {
                SelectedRecipeProductLookups = new ObservableCollection<RecipeProductLookup>(_recipesRepository
                .GetRecipeProductLookupsForRecipe(SelectedRecipe.Id));
            }
        }

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
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
        }

        public ICommand DiscardChanges { get; }
        private void OnDiscardChanges()
        {
            if (SelectedRecipe != null)
            {
                SelectedRecipe.Title = UnchangedSelectedRecipe.Title;
                SelectedRecipe.Description = UnchangedSelectedRecipe.Description;
                SelectedRecipe.RecipeProducts = new List<RecipeProduct>(UnchangedSelectedRecipe.RecipeProducts);

                Recipe manipulatedRecipe = Recipes.Where(recipe => recipe.Id == SelectedRecipe.Id).First();
                manipulatedRecipe.Title = UnchangedSelectedRecipe.Title;
                manipulatedRecipe.Description = UnchangedSelectedRecipe.Description;
                manipulatedRecipe.RecipeProducts = new List<RecipeProduct>(UnchangedSelectedRecipe.RecipeProducts);
            }
            EditMode = false;
            SelectedRecipe = null;
            SelectedRecipeProductLookups = null;
            //czyszczenie inputu usera
            SelectedSearchBoxProduct = new Product();
        }

        public ICommand ToggleEditMode { get; }
        private void OnToggleEditModeExecute()
        {
            EditMode = !EditMode;
            OnPropertyChanged();
        }

        private void OnRemoveRecipeProduct()
        {
            if (SelectedRecipeProductLookup != null)
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
            if (SelectedSearchBoxProduct != null
                && !SelectedRecipeProductLookups.Where(srpl => srpl.ProductId == SelectedSearchBoxProduct.Id).Any())
            {
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
        }
        public ICommand AddRecipe { get; }

        private void OnRemoveRecipe()
        {
            if (SelectedRecipe != null)
            {
                _recipesRepository.RemoveRecipe(SelectedRecipe);
                Recipes.Remove(SelectedRecipe);
                _recipesRepository.Save();
            }
        }
        public ICommand RemoveRecipe { get; }

        public async Task LoadAsync()
        {
            await Task.CompletedTask;
        }

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

        public ObservableCollection<ProductCategory> Categories { get; }

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

        }
        private bool CanAddCommandExecute()
        {
            return !String.IsNullOrWhiteSpace(NewProductName) && NewProductCategory != null;
        }
    }
}

