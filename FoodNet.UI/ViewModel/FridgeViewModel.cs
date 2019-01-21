using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;
using FoodNet.UI.Data.Interfaces;
using FoodNet.UI.View;
using System.Windows.Controls;
using Microsoft.Practices.Prism.PubSubEvents;
using FoodNet.UI.Event;

namespace FoodNet.UI.ViewModel
{
    public class FridgeViewModel : ViewModelBase
    {
        private IFridgeRepository _fridgeRepository;
        private IProductsRepository _productsRepository;
        private IEventAggregator _eventAggregator;

        public FridgeViewModel(IFridgeRepository fridgeRepository, IProductsRepository productsRepository, IEventAggregator eventAggregator)
        {
            _fridgeRepository = fridgeRepository;
            _productsRepository = productsRepository;
            _eventAggregator = eventAggregator;

            LoadDataCommand = new DelegateCommand(OnLoadDataExecute);
            DeleteFridgeProductCommand = new DelegateCommand<object>(OnDeleteFridgeProductExecute);
            AddFromSearchBoxCommand = new DelegateCommand(OnAddFromSearchBoxExecute);
            OpenNewProductDialogCommand = new DelegateCommand(OnOpenNewProductDialogExecute);
            AddCommand = new DelegateCommand(OnAddCommandExecute, CanAddCommandExecute);
            CancelCommand = new DelegateCommand(OnCancelCommandExecute);
            _eventAggregator.GetEvent<NewProductAddedEvent>().Subscribe(AfterNewProductAdded);
        }

        public ICommand LoadDataCommand { get; }
        private void OnLoadDataExecute()
        {
            Products = new ObservableCollection<Product>(_productsRepository.GetAllProducts());
            FridgeLookups = new ObservableCollection<FridgeProductLookup>(_fridgeRepository.GetAllFridgeLookups());
            Categories = new ObservableCollection<ProductCategory>(_productsRepository.GetAllCategories());
            Fridge = _fridgeRepository.GetFridge();

            if(Categories.Count > 0)
            {
                SetProductsByCategory(Categories[0]);
                SelectedCategory = Categories[0];
            }
        }
        private void SetProductsByCategory(ProductCategory category)
        {
            ProductsPerCategory = new ObservableCollection<Product>(Products.Where(p => p.ProductCategoryId == category.Id));
        }

        // Categories
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

        private ProductCategory _selectedCategory;
        public ProductCategory SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                SetProductsByCategory(_selectedCategory);
            }
        }
        // Products
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

        private ObservableCollection<Product> _productsPerCategory;
        public ObservableCollection<Product> ProductsPerCategory
        {
            get { return _productsPerCategory; }
            set
            {
                _productsPerCategory = value;
                OnPropertyChanged();
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                AddProductToFridge(_selectedProduct);
                OnPropertyChanged();
            }
        }

        private void AddProductToFridge(Product product)
        {
            if (product != null)
            {
                Guid newId = Guid.NewGuid();
                FridgeProductLookup newItem = new FridgeProductLookup
                {
                    ProductId = product.Id,
                    FridgeProductId = newId,
                    Name = product.Name,
                    Description = product.Description,
                };

                if (!FridgeLookups.Any(item => item.ProductId == newItem.ProductId))
                {
                    FridgeLookups.Add(newItem);
                    FridgeProduct newProduct = new FridgeProduct { Id = newId, ProductId = product.Id, FridgeId = Fridge.Id };
                    _fridgeRepository.AddProduct(newProduct);
                    _fridgeRepository.Save();
                }
            }
        }
        private void AfterNewProductAdded(NewProductAddedEventArgs obj)
        {
            Product newProduct = obj.NewProduct;
            if (Products != null)
                Products.Add(newProduct);
        }

        // Fridge
        private Fridge _fridge;
        public Fridge Fridge
        {
            get { return _fridge; }
            set
            {
                _fridge = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<FridgeProductLookup> _fridgeLookups;
        public ObservableCollection<FridgeProductLookup> FridgeLookups
        {
            get { return _fridgeLookups; }
            set
            {
                _fridgeLookups = value;
                OnPropertyChanged();
            }
        }

        public bool IsFridgeEmpty { get => FridgeLookups.Count == 0; }
        public bool IsFridgeNotEmpty { get => FridgeLookups.Count != 0; }

        public ICommand DeleteFridgeProductCommand { get; }
        private void OnDeleteFridgeProductExecute(object obj)
        {
            FridgeProductLookup selectedFrigeProduct = (FridgeProductLookup)obj;
            if (selectedFrigeProduct != null)
            {
                _fridgeRepository.DeleteProduct(selectedFrigeProduct.FridgeProductId);
                _fridgeRepository.Save();
                FridgeLookups.Remove(selectedFrigeProduct);
            }
        }
        // SearchBox
        public Product SelectedSearchBoxProduct { get; set; }

        public ICommand AddFromSearchBoxCommand { get; }
        private void OnAddFromSearchBoxExecute()
        {
            AddProductToFridge(SelectedSearchBoxProduct);
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
            IsDialogOpen = true;
            NewProductName = "";
            NewProductDescription = "";
            NewProductCategory = SelectedCategory;
            newProduct = null;
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
            if (SelectedCategory.Id == newProduct.ProductCategory.Id)
            {
                ProductsPerCategory.Add(newProduct);
            }
            _productsRepository.AddProduct(newProduct);
            _productsRepository.Save();

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
