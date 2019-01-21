using FoodNet.Model.Domain;
using FoodNet.UI.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FoodNet.UI.View
{

    public partial class NewProductDialog : Window
    {
        public NewProductDialog(ObservableCollection<ProductCategory> categories, ProductCategory defaultCategory)
        {
            InitializeComponent();
            AddCommand = new DelegateCommand(OnAddCommandExecute, CanAddCommandExecute);
            CancelCommand = new DelegateCommand(OnCancelCommandExecute);
            DataContext = this;
            Categories = categories;
            SelectedCategory = defaultCategory;
        }

        public Product newProduct { get; set; }
        public ObservableCollection<ProductCategory> Categories { get; set; }

        private ProductCategory _selectedCategory;
        public ProductCategory SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                ((DelegateCommand)AddCommand).RaiseCanExecuteChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        public ICommand CancelCommand { get; }
        private void OnCancelCommandExecute()
        {
            DialogResult = false;
            this.Close();
        }

        public ICommand AddCommand { get; }
        private void OnAddCommandExecute()
        {
            newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = ProductName,
                Description = Description,
                ProductCategory = SelectedCategory
            };
            DialogResult = true;
            this.Close();
        }
        private bool CanAddCommandExecute()
        {
            return !String.IsNullOrWhiteSpace(ProductName) && SelectedCategory != null;
        }

    }
}
