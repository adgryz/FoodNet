using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using FoodNet.UI.Data.Interfaces;
using FoodNet.UI.Event;
using FoodNet.UI.ViewModel;
using Microsoft.Practices.Prism.PubSubEvents;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FoodNet.Tests
{
    public class FridgeViewModelTests
    {
        // AddsNewProducts - TODO 
        // KnowsWhenToAllowToAddProduct - TODO

        Mock<IFridgeRepository> fridgeRepositoryMock;
        Mock<IEventAggregator> eventAggregatorMock;
        Mock<IProductsRepository> productsRepositoryMock;
        Mock<NewProductAddedEvent> eventMock;

        public FridgeViewModelTests()
        {
            fridgeRepositoryMock = new Mock<IFridgeRepository>();
            eventAggregatorMock = new Mock<IEventAggregator>();
            eventMock = new Mock<NewProductAddedEvent>();
            eventAggregatorMock.Setup(e => e.GetEvent<NewProductAddedEvent>()).
                Returns(eventMock.Object);
            productsRepositoryMock = new Mock<IProductsRepository>();
        }

        [Fact]
        public void LoadsProducts()
        {
            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
                Returns(new List<Product>
                {
                    new Product { Id = Guid.NewGuid()},
                    new Product { Id = Guid.NewGuid()}
                });
            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);

            vm.LoadDataCommand.Execute(null);

            Assert.Equal(2, vm.Products.Count);
        }

        [Fact]
        public void LoadsFridge()
        {
            fridgeRepositoryMock.Setup(fr => fr.GetFridge()).
                Returns(new Fridge
                {
                    FridgeProducts = new List<FridgeProduct>
                    {
                        new FridgeProduct{ Id = Guid.NewGuid()},
                        new FridgeProduct { Id = Guid.NewGuid() }
                    }

                });

            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);

            vm.LoadDataCommand.Execute(null);

            Assert.NotNull(vm.Fridge);
            Assert.NotNull(vm.Fridge.FridgeProducts);
            Assert.Equal(2, vm.Fridge.FridgeProducts.Count);
        }

        [Fact]
        public void LoadsCategories()
        {
            productsRepositoryMock.Setup(pr => pr.GetAllCategories()).
                Returns(new List<ProductCategory>
                {
                    new ProductCategory{Id = Guid.NewGuid()},
                    new ProductCategory{Id = Guid.NewGuid()}
                });

            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);

            vm.LoadDataCommand.Execute(null);

            Assert.NotNull(vm.Categories);
            Assert.Equal(2, vm.Categories.Count);
        }

        [Fact]
        public void SetDefaultCategoryAndItsProducts()
        {
            var meatId = Guid.NewGuid();
            var vegeId = Guid.NewGuid();
            var Vege = new ProductCategory { Id = vegeId, Name = "Vegetables" };
            var Meat = new ProductCategory { Id = meatId, Name = "Meat" };
            productsRepositoryMock.Setup(pr => pr.GetAllCategories()).
                Returns(new List<ProductCategory>
                {
                    Meat, Vege
                });

            var Bacon = new Product { Id = Guid.NewGuid(), Name = "Bacon", ProductCategoryId = meatId };
            var Beef = new Product { Id = Guid.NewGuid(), Name = "Beef", ProductCategoryId = meatId };
            var Tomato = new Product { Id = Guid.NewGuid(), Name = "Tomato", ProductCategoryId = vegeId };
            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
                Returns(new List<Product>
                {
                    Bacon, Beef, Tomato
                });

            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);

            vm.LoadDataCommand.Execute(null);
            Assert.Equal(2, vm.Categories.Count);
            Assert.Equal("Meat", vm.SelectedCategory.Name);
            Assert.Equal(2, vm.ProductsPerCategory.Count);
            Assert.Contains(Bacon, vm.ProductsPerCategory);
            Assert.Contains(Beef, vm.ProductsPerCategory);

        }

        [Fact]
        public void ChangeOfCategoryChangesListOfProducts()
        {
            var meatId = Guid.NewGuid();
            var vegeId = Guid.NewGuid();
            var Vege = new ProductCategory { Id = vegeId, Name = "Vegetables" };
            var Meat = new ProductCategory { Id = meatId, Name = "Meat" };
            productsRepositoryMock.Setup(pr => pr.GetAllCategories()).
                Returns(new List<ProductCategory>
                {
                    Meat, Vege
                });

            var Bacon = new Product { Id = Guid.NewGuid(), Name = "Bacon", ProductCategoryId = meatId };
            var Beef = new Product { Id = Guid.NewGuid(), Name = "Beef", ProductCategoryId = meatId };
            var Tomato = new Product { Id = Guid.NewGuid(), Name = "Tomato", ProductCategoryId = vegeId };
            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
                Returns(new List<Product>
                {
                    Bacon, Beef, Tomato
                });

            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);

            vm.LoadDataCommand.Execute(null);
            Assert.Equal(2, vm.Categories.Count);
            Assert.Equal("Meat", vm.SelectedCategory.Name);
            vm.SelectedCategory = Vege;
            Assert.Equal("Vegetables", vm.SelectedCategory.Name);
            Assert.Single(vm.ProductsPerCategory);
            Assert.Contains(Tomato, vm.ProductsPerCategory);
        }

        [Fact]
        public void AfterSelectionOfNewProductItsAddedToFridge()
        {
            fridgeRepositoryMock.Setup(fr => fr.GetFridge()).
                Returns(new Fridge
                {
                    FridgeProducts = new List<FridgeProduct>()
                });

            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);

            vm.LoadDataCommand.Execute(null);

            int countBefore = vm.FridgeLookups.Count;
            vm.SelectedProduct = new Product { Id = Guid.NewGuid() };
            int countAfter = vm.FridgeLookups.Count;

            Assert.Equal(0, countBefore);
            Assert.Equal(1, countAfter);
        }

        [Fact]
        public void AddProductToFridgeIsNullProtected()
        {
            fridgeRepositoryMock.Setup(fr => fr.GetFridge()).
                Returns(new Fridge
                {
                    FridgeProducts = new List<FridgeProduct>()
                });

            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);

            vm.LoadDataCommand.Execute(null);

            int countBefore = vm.FridgeLookups.Count;
            vm.SelectedProduct = null;
            int countAfter = vm.FridgeLookups.Count;

            Assert.Equal(countBefore, countAfter);
        }

        [Fact]
        public void AddingExistingProductToFridgeHasNoEffect()
        {
            var id = Guid.NewGuid();

            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);
            vm.LoadDataCommand.Execute(null);

            vm.FridgeLookups.Add(new FridgeProductLookup { ProductId = id });
            int countBefore = vm.FridgeLookups.Count;
            vm.SelectedProduct = new Product { Id = id };
            int countAfter = vm.FridgeLookups.Count;

            Assert.Equal(countBefore, countAfter);
        }

        [Fact]
        public void DeleteProductFromFridge()
        {
            fridgeRepositoryMock.Setup(fr => fr.GetFridge()).
                Returns(new Fridge
                {
                    FridgeProducts = new List<FridgeProduct>()
                });


            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);
            vm.LoadDataCommand.Execute(null);
            var prodId = Guid.NewGuid();
            var product = new Product { Id = prodId, Name = "RemProduct" };
            vm.SelectedProduct = product;
            Assert.Single(vm.FridgeLookups);
            var lookup = vm.FridgeLookups[0];
            vm.DeleteFridgeProductCommand.Execute(lookup);
            Assert.Empty(vm.FridgeLookups);
        }

        [Fact]
        public void AddProductToFridgeFromSearchbox()
        {
            var id = Guid.NewGuid();

            fridgeRepositoryMock.Setup(fr => fr.GetFridge()).
                Returns(new Fridge
                {
                    FridgeProducts = new List<FridgeProduct>()
                });

            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);
            vm.LoadDataCommand.Execute(null);
            vm.SelectedSearchBoxProduct = new Product { Id = id };
            Assert.Empty(vm.FridgeLookups);
            vm.AddFromSearchBoxCommand.Execute(null);
            Assert.Single(vm.FridgeLookups);
        }

        [Fact]
        public void AddNewProduct()
        {
            var id = Guid.NewGuid();
            var catId = Guid.NewGuid();
            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
             Returns(new List<Product>());

            productsRepositoryMock.Setup(pr => pr.GetAllCategories()).
                Returns(new List<ProductCategory>
                {
                                new ProductCategory{Id = catId},
                });


            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);
            vm.LoadDataCommand.Execute(null);
            vm.NewProductCategory = new ProductCategory { Id = catId };
            Assert.Empty(vm.Products);
            vm.AddCommand.Execute(null);
            Assert.Single(vm.Products);
        }

        [Fact]
        public void WhenAddNewProductCanceledThereIsNoNewProduct()
        {
            var id = Guid.NewGuid();
            var catId = Guid.NewGuid();
            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
             Returns(new List<Product>());

            productsRepositoryMock.Setup(pr => pr.GetAllCategories()).
                Returns(new List<ProductCategory>
                {
                                new ProductCategory{Id = catId},
                });


            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);
            vm.LoadDataCommand.Execute(null);
            vm.NewProductCategory = new ProductCategory { Id = catId };
            Assert.Empty(vm.Products);
            vm.CancelCommand.Execute(null);
            Assert.Empty(vm.Products);
        }

        [Fact]
        public void UserCantAddNewProductWithNullName()
        {
            var id = Guid.NewGuid();
            var catId = Guid.NewGuid();
            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
             Returns(new List<Product>());

            productsRepositoryMock.Setup(pr => pr.GetAllCategories()).
                Returns(new List<ProductCategory>
                {
                                new ProductCategory{Id = catId},
                });


            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);
            vm.LoadDataCommand.Execute(null);
            vm.NewProductName = null;
            vm.NewProductCategory = new ProductCategory { Id = catId };
            Assert.False(vm.AddCommand.CanExecute(null));
        }

        [Fact]
        public void UserCantAddNewProductWithEmptyName()
        {
            var id = Guid.NewGuid();
            var catId = Guid.NewGuid();
            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
             Returns(new List<Product>());

            productsRepositoryMock.Setup(pr => pr.GetAllCategories()).
                Returns(new List<ProductCategory>
                {
                                new ProductCategory{Id = catId},
                });


            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);
            vm.LoadDataCommand.Execute(null);
            vm.NewProductName = "  ";
            vm.NewProductCategory = new ProductCategory { Id = catId };
            Assert.False(vm.AddCommand.CanExecute(null));
        }

        [Fact]
        public void UserCantAddNewProductWithNullCategory()
        {
            var id = Guid.NewGuid();
            var catId = Guid.NewGuid();
            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
             Returns(new List<Product>());

            productsRepositoryMock.Setup(pr => pr.GetAllCategories()).
                Returns(new List<ProductCategory>
                {
                                new ProductCategory{Id = catId},
                });


            FridgeViewModel vm = new FridgeViewModel(fridgeRepositoryMock.Object,
                                                     productsRepositoryMock.Object,
                                                     eventAggregatorMock.Object);
            vm.LoadDataCommand.Execute(null);
            vm.NewProductCategory = null;
            vm.NewProductCategory = new ProductCategory { Id = catId };
            Assert.False(vm.AddCommand.CanExecute(null));
        }


    }
}
