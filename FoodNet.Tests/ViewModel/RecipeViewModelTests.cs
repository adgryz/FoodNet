using FoodNet.Model.Domain;
using FoodNet.Model.Lookups;
using FoodNet.UI.Data.Interfaces;
using FoodNet.UI.Event;
using FoodNet.UI.ViewModel;
using Microsoft.Practices.Prism.PubSubEvents;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodNet.Tests.ViewModel
{
    public class RecipeViewModelTests
    {
        private Mock<IRecipesRepository> recipesRepositoryMock;
        private Mock<IProductsRepository> productsRepositoryMock;
        private Mock<IUsersRepository> usersRepositoryMock;
        private Mock<IEventAggregator> eventAggregatorMock;
        private RecipesViewModel recipesViewModel;

        private List<Recipe> recipeList;
        private List<Product> productList;
        private List<RecipeProduct> recipeProductList;
        private Recipe recipe1;
        private Recipe recipe2;

        private Product product1;
        private Product product2;

        private RecipeProductLookup rpl1;
        private RecipeProductLookup rpl2;
        private RecipeProductLookup rpl3;

        
        private Boolean repoSaved;

        public RecipeViewModelTests()
        {
            setUpMocks();
        }

        private void setUpMocks()
        {
            repoSaved = false;

            recipesRepositoryMock = new Mock<IRecipesRepository>();
            productsRepositoryMock = new Mock<IProductsRepository>();
            usersRepositoryMock = new Mock<IUsersRepository>();
            eventAggregatorMock = new Mock<IEventAggregator>();

            var productAddedEventMock = new Mock<NewProductAddedEvent>();
            eventAggregatorMock.Setup(e => e.GetEvent<NewProductAddedEvent>()).
                Returns(productAddedEventMock.Object);
            var recipeAddedEventMock = new Mock<NewRecipeAddedEvent>();
            eventAggregatorMock.Setup(e => e.GetEvent<NewRecipeAddedEvent>()).
                Returns(recipeAddedEventMock.Object);
            var recipeRemovedEventMock = new Mock<RecipeRemovedEvent>();
            eventAggregatorMock.Setup(e => e.GetEvent<RecipeRemovedEvent>()).
                Returns(recipeRemovedEventMock.Object);
            var recipeUpdatedEventMock = new Mock<RecipeUpdatedEvent>();
            eventAggregatorMock.Setup(e => e.GetEvent<RecipeUpdatedEvent>()).
                Returns(recipeUpdatedEventMock.Object);

            recipeProductList = new List<RecipeProduct>();

            product1 = new Product { Id = Guid.NewGuid(), Name = "Product1" };
            product2 = new Product { Id = Guid.NewGuid(), Name = "Product2" };
            productList = new List<Product> { product1, product2 };

            productsRepositoryMock.Setup(pr => pr.GetAllProducts()).
                Returns(productList);

            User user = new User { Id = Guid.NewGuid() };
            usersRepositoryMock.Setup(ur => ur.GetFirstUser())
                .Returns(user);

            recipe1 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Recipe1",
                UserId = user.Id
            };
            recipe2 = new Recipe {
                Id = Guid.NewGuid(),
                Title = "Recipe2",
                UserId = user.Id
            };

            recipeList = new List<Recipe> { recipe1, recipe2 };

            recipesRepositoryMock.Setup(rr => rr.GetAllRecipes())
                .Returns(recipeList);

            recipesRepositoryMock.Setup(rr => rr.Save())
                .Callback(() => repoSaved = true);

            RecipeProduct recipeProduct1 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product1.Id,
                RecipeId = recipe1.Id
            };
            RecipeProduct recipeProduct2 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product2.Id,
                RecipeId = recipe1.Id
            };
            RecipeProduct recipeProduct3 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product1.Id,
                RecipeId = recipe2.Id
            };

            rpl1 = new RecipeProductLookup
            {
                ProductId = product1.Id,
                RecipeProductId = recipeProduct1.Id,
                ProductName = product1.Name
            };
            rpl2 = new RecipeProductLookup
            {
                ProductId = product2.Id,
                RecipeProductId = recipeProduct2.Id,
                ProductName = product2.Name
            };
            rpl3 = new RecipeProductLookup
            {
                ProductId = product1.Id,
                RecipeProductId = recipeProduct3.Id,
                ProductName = product1.Name
            };


            recipe1.RecipeProducts = new List<RecipeProduct> {
                recipeProduct1, recipeProduct2
                };
            recipe2.RecipeProducts = new List<RecipeProduct> {
                recipeProduct3
                };

            recipesRepositoryMock.Setup(rr => rr.AddRecipe(
                It.IsAny<Recipe>()))
                .Callback<Recipe>(r => recipeList.Add(r));
            recipesRepositoryMock.Setup(rr => rr.RemoveRecipe(
                It.IsAny<Recipe>()))
                .Callback<Recipe>(r => recipeList.Remove(r));

            recipesRepositoryMock.Setup(rr => rr.GetProductsForRecipe(
                    It.Is<Guid>(g => g.Equals(product1.Id))))
                .Returns(new List<Product>
                {
                    product1, product2
                });
            recipesRepositoryMock.Setup(rr => rr.GetRecipeProductLookupsForRecipe(
                    It.Is<Guid>(g => g.Equals(recipe1.Id))))
                .Returns(new List<RecipeProductLookup>{ rpl1, rpl2 });

            recipesRepositoryMock.Setup(rr => rr.GetProductsForRecipe(
                    It.Is<Guid>(g => g.Equals(product2.Id))))
                .Returns(new List<Product> { product1});

            recipesRepositoryMock.Setup(rr => rr.GetRecipeProductLookupsForRecipe(
                    It.Is<Guid>(g => g.Equals(recipe2.Id))))
                .Returns(new List<RecipeProductLookup>{ rpl3 });

            recipesRepositoryMock.Setup(rr => rr.GetProductsForRecipe(
                    It.Is<Guid>(g => !g.Equals(recipe1.Id) && !g.Equals(recipe2.Id))))
                .Returns(new List<Product>());
            recipesRepositoryMock.Setup(rr => rr.GetRecipeProductLookupsForRecipe(
                    It.Is<Guid>(g => !g.Equals(recipe1.Id) && !g.Equals(recipe2.Id))))
                .Returns(new List<RecipeProductLookup>());

            recipesRepositoryMock.Setup(rr => rr.AddRecipeProduct(
                    It.IsAny<RecipeProduct>()))
                    .Callback<RecipeProduct>(rp => recipeProductList.Add(rp));
                    


            recipesViewModel = new RecipesViewModel(
                recipesRepositoryMock.Object,
                productsRepositoryMock.Object,
                usersRepositoryMock.Object,
                eventAggregatorMock.Object);

        }

        [Fact]
        public void LoadsProducts()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);
            Assert.Equal(2, recipesViewModel.Products.Count);
            Assert.Equal(2, recipesViewModel.Recipes.Count);
        }

        [Fact]
        public void UpdatesSelectedRecipeProductLookups()
        {
            setUpMocks();
            Recipe recipe = recipesRepositoryMock.Object.GetAllRecipes()
                .Where(r => r.Title == "Recipe1").First();
            Recipe recipe2 = recipesRepositoryMock.Object.GetAllRecipes()
                .Where(r => r.Title == "Recipe2").First();

            Boolean b = recipe1.Id.Equals(recipe.Id);
            Boolean c = recipe.Id.Equals(recipe1.Id);

            recipesViewModel.SelectedRecipe = recipe;
            Assert.Equal(2, recipesViewModel.SelectedRecipeProductLookups.Count());
            recipesViewModel.SelectedRecipe = recipe2;         
            Assert.Single(recipesViewModel.SelectedRecipeProductLookups);
            recipesViewModel.SelectedRecipe = new Recipe {
                Id = Guid.NewGuid(),
                UserId = usersRepositoryMock.Object.GetFirstUser().Id };
            Assert.Empty(recipesViewModel.SelectedRecipeProductLookups);
        }

        [Fact]
        public void AddRecipeCommandAddsToRecipesAndRepo()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            int recipesCount = recipesViewModel.Recipes.Count();
            int recipesListCount = recipeList.Count();
            recipesViewModel.AddRecipe.Execute(null);
            Assert.Equal(recipesCount + 1, recipesViewModel.Recipes.Count());
            Assert.Equal(recipesListCount + 1, recipeList.Count());
            Assert.True(repoSaved);
        }

        [Fact]
        public void RemoveRecipeCommandRemovesFromRecipesAndRepo()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;

            int recipesCount = recipesViewModel.Recipes.Count();
            int recipesListCount = recipeList.Count();
            recipesViewModel.RemoveRecipe.Execute(null);
            Assert.Equal(recipesCount - 1, recipesViewModel.Recipes.Count());
            Assert.Equal(recipesListCount - 1, recipeList.Count());
            Assert.True(repoSaved);
        }

        [Fact]
        public void RemoveRecipeIsNullProtected()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = null;

            int recipesCount = recipesViewModel.Recipes.Count();
            int recipesListCount = recipeList.Count();
            recipesViewModel.RemoveRecipe.Execute(null);
            Assert.Equal(recipesCount, recipesViewModel.Recipes.Count());
            Assert.Equal(recipesListCount, recipeList.Count());
            Assert.False(repoSaved);
        }

        [Fact]
        public void AddNewRecipeProductAddsToRecipeProduct()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedSearchBoxProduct = product2;
            recipesViewModel.SelectedRecipe = recipe2;

            int recipesProductCount = recipesViewModel.SelectedRecipe.RecipeProducts.Count();

            recipesViewModel.AddRecipeProduct.Execute(null);

            Assert.Equal(recipesProductCount + 1, recipesViewModel.SelectedRecipe.RecipeProducts.Count());
        }

        [Fact]
        public void AddNewRecipeProductAddsToLookups()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedSearchBoxProduct = product2;
            recipesViewModel.SelectedRecipe = recipe2;

            int recipesProductLookupsCount = recipesViewModel.SelectedRecipeProductLookups.Count();

            recipesViewModel.AddRecipeProduct.Execute(null);

            Assert.Equal(recipesProductLookupsCount + 1, 
                recipesViewModel.SelectedRecipeProductLookups.Count());
        }

        [Fact]
        public void AddNewRecipeProductsDoesntCallRepo()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedSearchBoxProduct = product2;
            recipesViewModel.SelectedRecipe = recipe2;
            
            int recipeProductListCount = recipeProductList.Count();

            recipesViewModel.AddRecipeProduct.Execute(null);
            Assert.Equal(recipeProductListCount, recipeProductList.Count());
        }

        [Fact]
        public void AddExistingRecipeProductHasNoEffect()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedSearchBoxProduct = product1;
            recipesViewModel.SelectedRecipe = recipe2;

            int recipesProductCount = recipesViewModel.SelectedRecipe.RecipeProducts.Count();
            int recipeProductListCount = recipeProductList.Count();

            recipesViewModel.AddRecipeProduct.Execute(null);

            Assert.Equal(recipesProductCount, recipesViewModel.SelectedRecipe.RecipeProducts.Count());
        }

        [Fact]
        public void AddRecipeProductIsSelectedRecipeNullProtected()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedSearchBoxProduct = product1;
            recipesViewModel.SelectedRecipe = null;

            int firstRecipeProductsCount = recipe1.RecipeProducts.Count();
            int secondRecipeProductsCount = recipe2.RecipeProducts.Count();

            recipesViewModel.AddRecipeProduct.Execute(null);

            Assert.Equal(firstRecipeProductsCount, recipe1.RecipeProducts.Count());
            Assert.Equal(secondRecipeProductsCount, recipe2.RecipeProducts.Count());
        }

        [Fact]
        public void AddRecipeProductIsSelectedProductNullProtected()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedSearchBoxProduct = null;
            recipesViewModel.SelectedRecipe = recipe1;

            int firstRecipeProductsCount = recipe1.RecipeProducts.Count();
            int secondRecipeProductsCount = recipe2.RecipeProducts.Count();

            recipesViewModel.AddRecipeProduct.Execute(null);

            Assert.Equal(firstRecipeProductsCount, recipe1.RecipeProducts.Count());
            Assert.Equal(secondRecipeProductsCount, recipe2.RecipeProducts.Count());
        }

        [Fact]
        public void AddRecipeProductIsEmptySelectedProductProtected()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedSearchBoxProduct = new Product
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000")
            };
            recipesViewModel.SelectedRecipe = recipe1;

            int firstRecipeProductsCount = recipe1.RecipeProducts.Count();
            int secondRecipeProductsCount = recipe2.RecipeProducts.Count();

            recipesViewModel.AddRecipeProduct.Execute(null);

            Assert.Equal(firstRecipeProductsCount, recipe1.RecipeProducts.Count());
            Assert.Equal(secondRecipeProductsCount, recipe2.RecipeProducts.Count());
        }

        [Fact]
        public void RemoveRecipeProductRemovesFromSelectedRecipe()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);
            
            recipesViewModel.SelectedRecipe = recipe1;
            recipesViewModel.SelectedRecipeProductLookup = recipesViewModel.SelectedRecipeProductLookups.First();

            int recipesProductCount = recipesViewModel.SelectedRecipe.RecipeProducts.Count();

            recipesViewModel.RemoveRecipeProduct.Execute(null);

            Assert.Equal(recipesProductCount - 1, recipesViewModel.SelectedRecipe.RecipeProducts.Count());
        }

        [Fact]
        public void RemoveRecipeProductIsSelectedProductLookupNullProtected()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);
            
            recipesViewModel.SelectedRecipe = recipe1;
            recipesViewModel.SelectedRecipeProductLookup = null;

            int firstRecipeProductsCount = recipe1.RecipeProducts.Count();
            int secondRecipeProductsCount = recipe2.RecipeProducts.Count();

            recipesViewModel.RemoveRecipeProduct.Execute(null);

            Assert.Equal(firstRecipeProductsCount, recipe1.RecipeProducts.Count());
            Assert.Equal(secondRecipeProductsCount, recipe2.RecipeProducts.Count());
        }

        [Fact]
        public void RemoveRecipeProductIsSelectedRecipeNullProtected()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;
            recipesViewModel.SelectedRecipeProductLookup = recipesViewModel.SelectedRecipeProductLookups.First();
            recipesViewModel.SelectedRecipe = null;

            int firstRecipeProductsCount = recipe1.RecipeProducts.Count();
            int secondRecipeProductsCount = recipe2.RecipeProducts.Count();

            recipesViewModel.RemoveRecipeProduct.Execute(null);

            Assert.Equal(firstRecipeProductsCount, recipe1.RecipeProducts.Count());
            Assert.Equal(secondRecipeProductsCount, recipe2.RecipeProducts.Count());
        }

        [Fact]
        public void RemoveRecipeProductRemovesFromLookups()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;
            recipesViewModel.SelectedRecipeProductLookup = recipesViewModel.SelectedRecipeProductLookups.First();

            int recipesProductLookupCount = recipesViewModel.SelectedRecipeProductLookups.Count();

            recipesViewModel.RemoveRecipeProduct.Execute(null);

            Assert.Equal(recipesProductLookupCount - 1, recipesViewModel.SelectedRecipeProductLookups.Count());
        }

        [Fact]
        public void RemoveRecipeProductDoesntCallRepo()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedSearchBoxProduct = product2;
            recipesViewModel.SelectedRecipe = recipe1;

            int recipeProductListCount = recipeProductList.Count();

            recipesViewModel.RemoveRecipeProduct.Execute(null);

            Assert.Equal(recipeProductListCount, recipeProductList.Count());
            Assert.False(repoSaved);
        }

        [Fact]
        public void SaveChangesNoticeChanges()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;
            recipesViewModel.EditMode = true;
            recipesViewModel.SelectedRecipe.Title = "some new changed title";
            recipesViewModel.SelectedRecipe.Description = "some new desc";
            recipesViewModel.SaveChanges.Execute(null);

            Assert.Equal("some new changed title", recipesViewModel.SelectedRecipe.Title);
            Assert.Equal("some new desc", recipesViewModel.SelectedRecipe.Description);
        }

        [Fact]
        public void DiscardChangesSetSelectedAndUnchangedRecipeToNull()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;
            String title = recipe1.Title;
            String desc = recipe1.Description;
            recipesViewModel.EditMode = true;
            recipesViewModel.SelectedRecipe.Title = "some new changed title";
            recipesViewModel.SelectedRecipe.Description = "some new desc";
            recipesViewModel.DiscardChanges.Execute(null);

            Assert.Null(recipesViewModel.SelectedRecipe);
            Assert.Null(recipesViewModel.UnchangedSelectedRecipe);
        }

        [Fact]
        public void DiscardChangesDiscards()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;
            Guid modifiedRecipeId = recipe1.Id;

            String title = recipe1.Title;
            String desc = recipe1.Description;
            recipesViewModel.EditMode = true;
            recipesViewModel.SelectedRecipe.Title = "some new changed title";
            recipesViewModel.SelectedRecipe.Description = "some new desc";
            recipesViewModel.DiscardChanges.Execute(null);

            Recipe modifiedRecipe = recipesViewModel.Recipes.Where(r => r.Id.Equals(modifiedRecipeId)).Single();

            Assert.Equal(title, modifiedRecipe.Title);
            Assert.Equal(desc, modifiedRecipe.Description);
        }

        [Fact]
        public void EditModeSetsUnchangedRecipe()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;
            recipesViewModel.EditMode = true;
            Assert.NotNull(recipesViewModel.UnchangedSelectedRecipe);
        }

        [Fact]
        public void SettingEditModeSelectedRecipeNullProtected()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            bool editMode = recipesViewModel.EditMode;
            recipesViewModel.SelectedRecipe = null;
            recipesViewModel.EditMode = true;
            Assert.Equal(editMode, recipesViewModel.EditMode);
            recipesViewModel.EditMode = false;
            Assert.Equal(editMode, recipesViewModel.EditMode);
        }

        [Fact]
        public void SettingSelectedRecipeToNullSetsUnchangedRecipeToNull()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = null;
            Assert.Null(recipesViewModel.UnchangedSelectedRecipe);
        }

        [Fact]
        public void DiscardChangesRestoresRecipeProducts()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;
            Guid modifiedRecipeId = recipe1.Id;

            List<RecipeProduct> recipeProductList = recipesViewModel.SelectedRecipe.RecipeProducts;
            recipesViewModel.SelectedRecipe.RecipeProducts.RemoveAt(0);

            Recipe modifiedRecipe = recipesViewModel.Recipes.Where(r => r.Id.Equals(modifiedRecipeId)).Single();

            Assert.Equal(recipeProductList, modifiedRecipe.RecipeProducts);
        }

        [Fact]
        public void DiscardChangesRestoresRecipeProducts2()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe1;
            recipesViewModel.SelectedRecipeProductLookup = recipesRepositoryMock.Object
                .GetRecipeProductLookupsForRecipe(recipe1.Id).First();
            Guid modifiedRecipeId = recipe1.Id;

            List<RecipeProduct> recipeProductList = recipesViewModel.SelectedRecipe.RecipeProducts;
            recipesViewModel.RemoveRecipeProduct.Execute(null);

            Recipe modifiedRecipe = recipesViewModel.Recipes.Where(r => r.Id.Equals(modifiedRecipeId)).Single();

            Assert.Equal(recipeProductList, modifiedRecipe.RecipeProducts);
        }

        [Fact]
        public void DiscardChangesRestoresRecipeProducts3()
        {
            setUpMocks();
            recipesViewModel.LoadDataCommand.Execute(null);

            recipesViewModel.SelectedRecipe = recipe2;
            recipesViewModel.SelectedSearchBoxProduct = product2;
            Guid modifiedRecipeId = recipe2.Id;

            List<RecipeProduct> recipeProductList = recipesViewModel.SelectedRecipe.RecipeProducts;
            recipesViewModel.AddRecipeProduct.Execute(null);

            Recipe modifiedRecipe = recipesViewModel.Recipes.Where(r => r.Id.Equals(modifiedRecipeId)).Single();

            Assert.Equal(recipeProductList, modifiedRecipe.RecipeProducts);
        }
    }
}

