using FoodNet.DataAccess;
using FoodNet.Model.Domain;
using FoodNet.UI.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodNet.Tests.Repository
{
    public class ResultDataServiceTests
    {

        private Mock<FoodNetDbContext> dbContextMock;
        private Mock<DbSet<RecipeProduct>> recipeProductDbSetMock;
        private Mock<DbSet<FridgeProduct>> fridgeProductDbSetMock;
        private Mock<DbSet<Recipe>> recipeDbSetMock;

        private User user;

        private Recipe recipe1;
        private Recipe recipe2;
        private Recipe recipe3;
        private Recipe recipe4;

        private ProductCategory productCategory;

        private Product product1;
        private Product product2;
        private Product product3;
        private Product product4;

        private Fridge fridge;

        private FridgeProduct fridgeProduct1;
        private FridgeProduct fridgeProduct2;
        private FridgeProduct fridgeProduct3;
        private FridgeProduct fridgeProduct4;

        private RecipeProduct recipe1Product1;
        private RecipeProduct recipe1Product2;
        private RecipeProduct recipe1Product3;
        private RecipeProduct recipe1Product4;

        private RecipeProduct recipe2Product1;
        private RecipeProduct recipe2Product2;
        private RecipeProduct recipe2Product3;
        private RecipeProduct recipe2Product4;

        private RecipeProduct recipe3Product1;
        private RecipeProduct recipe3Product2;
        private RecipeProduct recipe3Product3;
        private RecipeProduct recipe3Product4;

        private RecipeProduct recipe4Product1;
        private RecipeProduct recipe4Product2;
        private RecipeProduct recipe4Product3;
        private RecipeProduct recipe4Product4;

        private List<FridgeProduct> fridgeProductForDbSetList;
        private List<RecipeProduct> recipeProductForDbSetList;
        private List<Recipe> recipeForDbSetList;

        private Func<FoodNetDbContext> dbContextCreator;

        private FoodNetDbContext DbContextGetter()
        {
            return dbContextMock.Object;
        }

        private void setUpMocks()
        {
            fridgeProductForDbSetList = new List<FridgeProduct>();
            recipeProductForDbSetList = new List<RecipeProduct>();
            recipeForDbSetList = new List<Recipe>();


            user = new User
            {
                Id = Guid.NewGuid(),
                Fridges = new List<Fridge>()
            };

            recipe1 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Recipe1",
                UserId = user.Id,
                Description = "test",
                RecipeProducts = new List<RecipeProduct>()
            };
            recipe2 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Recipe2",
                UserId = user.Id,
                Description = "test",
                RecipeProducts = new List<RecipeProduct>()
            };
            recipe3 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Recipe3",
                UserId = user.Id,
                Description = "test",
                RecipeProducts = new List<RecipeProduct>()
            };
            recipe4 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Recipe4",
                UserId = user.Id,
                Description = "test",
                RecipeProducts = new List<RecipeProduct>()
            };

            productCategory = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "ProductCategory1"
            };

            product1 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product1",
                ProductCategoryId = productCategory.Id,
                Description = "test"
            };
            product2 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product2",
                ProductCategoryId = productCategory.Id,
                Description = "test"
            };
            product3 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product3",
                ProductCategoryId = productCategory.Id,
                Description = "test"
            };
            product4 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product4",
                ProductCategoryId = productCategory.Id,
                Description = "test"
            };

            fridge = new Fridge
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                FridgeProducts = new List<FridgeProduct>()
            };

            fridgeProduct1 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge.Id,
                ProductId = product1.Id
            };
            fridgeProduct2 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge.Id,
                ProductId = product2.Id
            };
            fridgeProduct3 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge.Id,
                ProductId = product3.Id
            };
            fridgeProduct4 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge.Id,
                ProductId = product4.Id
            };

            recipe1Product1 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product1.Id,
                RecipeId = recipe1.Id,
                Recipe = recipe1,
                Product = product1
            };
            recipe1Product2 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product2.Id,
                RecipeId = recipe1.Id,
                Recipe = recipe1,
                Product = product2
            };
            recipe1Product3 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product3.Id,
                RecipeId = recipe1.Id,
                Recipe = recipe1,
                Product = product3
            };
            recipe1Product4 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product4.Id,
                RecipeId = recipe1.Id,
                Recipe = recipe1,
                Product = product4
            };

            recipe2Product1 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product1.Id,
                RecipeId = recipe2.Id,
                Recipe = recipe2,
                Product = product1
            };
            recipe2Product2 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product2.Id,
                RecipeId = recipe2.Id,
                Recipe = recipe2,
                Product = product2
            };
            recipe2Product3 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product3.Id,
                RecipeId = recipe2.Id,
                Recipe = recipe2,
                Product = product3
            };
            recipe2Product4 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product4.Id,
                RecipeId = recipe2.Id,
                Recipe = recipe2,
                Product = product4
            };

            recipe3Product1 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product1.Id,
                RecipeId = recipe3.Id,
                Recipe = recipe3,
                Product = product1
            };
            recipe3Product2 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product2.Id,
                RecipeId = recipe3.Id,
                Recipe = recipe3,
                Product = product2
            };
            recipe3Product3 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product3.Id,
                RecipeId = recipe3.Id,
                Recipe = recipe3,
                Product = product3
            };
            recipe3Product4 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product4.Id,
                RecipeId = recipe3.Id,
                Recipe = recipe3,
                Product = product4
            };

            recipe4Product1 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product1.Id,
                RecipeId = recipe4.Id,
                Recipe = recipe4,
                Product = product1
            };
            recipe4Product2 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product2.Id,
                RecipeId = recipe4.Id,
                Recipe = recipe4,
                Product = product2
            };
            recipe4Product3 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product3.Id,
                RecipeId = recipe4.Id,
                Recipe = recipe4,
                Product = product3
            };
            recipe4Product4 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                ProductId = product4.Id,
                RecipeId = recipe4.Id,
                Recipe = recipe4,
                Product = product4
            };

        }

        private void setUpDbSets()
        {


            recipeProductDbSetMock = new Mock<DbSet<RecipeProduct>>();
            fridgeProductDbSetMock = new Mock<DbSet<FridgeProduct>>();
            recipeDbSetMock = new Mock<DbSet<Recipe>>();



            var queryable = recipeForDbSetList.AsQueryable<Recipe>();

            recipeDbSetMock.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(queryable.Provider);
            recipeDbSetMock.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(queryable.Expression);
            recipeDbSetMock.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            recipeDbSetMock.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            recipeDbSetMock.Setup(d => d.Add(It.IsAny<Recipe>())).Callback<Recipe>((s) => recipeForDbSetList.Add(s));
            recipeDbSetMock.Setup(m => m.Include(It.IsAny<string>())).Returns(recipeDbSetMock.Object);

            //--

            var queryable2 = recipeProductForDbSetList.AsQueryable<RecipeProduct>();

            recipeProductDbSetMock.As<IQueryable<RecipeProduct>>().Setup(m => m.Provider).Returns(queryable2.Provider);
            recipeProductDbSetMock.As<IQueryable<RecipeProduct>>().Setup(m => m.Expression).Returns(queryable2.Expression);
            recipeProductDbSetMock.As<IQueryable<RecipeProduct>>().Setup(m => m.ElementType).Returns(queryable2.ElementType);
            recipeProductDbSetMock.As<IQueryable<RecipeProduct>>().Setup(m => m.GetEnumerator()).Returns(() => queryable2.GetEnumerator());
            recipeProductDbSetMock.Setup(d => d.Add(It.IsAny<RecipeProduct>())).Callback<RecipeProduct>((s) => recipeProductForDbSetList.Add(s));
            recipeProductDbSetMock.Setup(m => m.Include(It.IsAny<string>())).Returns(recipeProductDbSetMock.Object);
            //--

            var queryable3 = fridgeProductForDbSetList.AsQueryable<FridgeProduct>();

            fridgeProductDbSetMock.As<IQueryable<FridgeProduct>>().Setup(m => m.Provider).Returns(queryable3.Provider);
            fridgeProductDbSetMock.As<IQueryable<FridgeProduct>>().Setup(m => m.Expression).Returns(queryable3.Expression);
            fridgeProductDbSetMock.As<IQueryable<FridgeProduct>>().Setup(m => m.ElementType).Returns(queryable3.ElementType);
            fridgeProductDbSetMock.As<IQueryable<FridgeProduct>>().Setup(m => m.GetEnumerator()).Returns(() => queryable3.GetEnumerator());
            fridgeProductDbSetMock.Setup(d => d.Add(It.IsAny<FridgeProduct>())).Callback<FridgeProduct>((s) => fridgeProductForDbSetList.Add(s));
            fridgeProductDbSetMock.Setup(m => m.Include(It.IsAny<string>())).Returns(fridgeProductDbSetMock.Object);

            recipeProductDbSetMock.Object.AddRange(recipeProductForDbSetList);
            fridgeProductDbSetMock.Object.AddRange(fridgeProductForDbSetList);
            recipeDbSetMock.Object.AddRange(recipeForDbSetList);

            dbContextMock = new Mock<FoodNetDbContext>();
            dbContextMock.Object.RecipeProducts = recipeProductDbSetMock.Object;
            dbContextMock.Object.FridgeProducts = fridgeProductDbSetMock.Object;
            dbContextMock.Object.Recipes = recipeDbSetMock.Object;
            
            dbContextCreator = DbContextGetter;
        }

        [Fact]
        public void FridgeHasAllProductsSoAllRecipesAreAvailable()
        {
            setUpMocks();

            //set up collections
            fridge.FridgeProducts.Add(fridgeProduct1);
            fridge.FridgeProducts.Add(fridgeProduct2);
            fridge.FridgeProducts.Add(fridgeProduct3);
            fridge.FridgeProducts.Add(fridgeProduct4);

            recipe1.RecipeProducts.Add(recipe1Product1);
            recipe1.RecipeProducts.Add(recipe1Product2);
            recipe1.RecipeProducts.Add(recipe1Product3);
            recipe1.RecipeProducts.Add(recipe1Product4);
            recipeForDbSetList.Add(recipe1);

            recipeForDbSetList.Add(recipe2);

            recipe3.RecipeProducts.Add(recipe3Product1);
            recipeForDbSetList.Add(recipe3);

            recipe4.RecipeProducts.Add(recipe4Product1);
            recipeForDbSetList.Add(recipe4);
            // fill db sets
            setUpDbSets();

            ResultDataService resultDataService = new ResultDataService(dbContextCreator);
            List<Recipe> resultRecipeList = resultDataService.GetRecipes().ToList();

            Assert.Equal(recipeForDbSetList, resultRecipeList);
        }

        [Fact]
        public void OneMatchingRecipe()
        {
            setUpMocks();

            //set up collections
            fridge.FridgeProducts.Add(fridgeProduct1);

            recipe1.RecipeProducts.Add(recipe1Product1);
            recipe2.RecipeProducts.Add(recipe2Product2);
            recipe3.RecipeProducts.Add(recipe3Product3);
            recipe4.RecipeProducts.Add(recipe4Product4);

            fridgeProductForDbSetList = new List<FridgeProduct> { fridgeProduct1 };
            recipeProductForDbSetList = new List<RecipeProduct> { recipe1Product1, recipe2Product2,
                recipe3Product3, recipe4Product4};
            recipeForDbSetList = new List<Recipe> { recipe1, recipe2, recipe3, recipe4 };
            //fillDbSets
            setUpDbSets();

            List<Recipe> expectedList = new List<Recipe> { recipe1 };

            ResultDataService resultDataService = new ResultDataService(dbContextCreator);
            List<Recipe> resultList = resultDataService.GetRecipes().ToList();

            Assert.Equal(expectedList, resultList);
        }

        [Fact]
        public void TwoMatchingRecipes()
        {
            setUpMocks();

            //set up collections
            fridge.FridgeProducts.Add(fridgeProduct1);
            fridge.FridgeProducts.Add(fridgeProduct2);
            fridgeProductForDbSetList = new List<FridgeProduct> { fridgeProduct1, fridgeProduct2 };

            recipe1.RecipeProducts.Add(recipe1Product1);
            recipe2.RecipeProducts.Add(recipe2Product2);
            recipe3.RecipeProducts.Add(recipe3Product3);
            recipe4.RecipeProducts.Add(recipe4Product4);
            recipeProductForDbSetList = new List<RecipeProduct> { recipe1Product1, recipe2Product2,
                recipe3Product3, recipe4Product4};
            recipeForDbSetList = new List<Recipe> { recipe1, recipe2, recipe3, recipe4 };
            //fillDbSets
            setUpDbSets();

            List<Recipe> expectedList = new List<Recipe> { recipe1, recipe2 };

            ResultDataService resultDataService = new ResultDataService(dbContextCreator);
            List<Recipe> resultList = resultDataService.GetRecipes().ToList();

            Assert.Equal(expectedList, resultList);
        }

        [Fact]
        public void ThreeMatchingRecipes()
        {
            setUpMocks();

            //set up collections
            fridge.FridgeProducts.Add(fridgeProduct1);
            fridge.FridgeProducts.Add(fridgeProduct2);
            fridge.FridgeProducts.Add(fridgeProduct3);
            fridgeProductForDbSetList = new List<FridgeProduct> {
                fridgeProduct1,
                fridgeProduct2,
                fridgeProduct3
            };

            recipe1.RecipeProducts.Add(recipe1Product1);
            recipe2.RecipeProducts.Add(recipe2Product2);
            recipe3.RecipeProducts.Add(recipe3Product3);
            recipe4.RecipeProducts.Add(recipe4Product4);
            recipeProductForDbSetList = new List<RecipeProduct> { recipe1Product1, recipe2Product2,
                recipe3Product3, recipe4Product4};
            recipeForDbSetList = new List<Recipe> { recipe1, recipe2, recipe3, recipe4 };
            //fillDbSets
            setUpDbSets();

            List<Recipe> expectedList = new List<Recipe> { recipe1, recipe2, recipe3 };

            ResultDataService resultDataService = new ResultDataService(dbContextCreator);
            List<Recipe> resultList = resultDataService.GetRecipes().ToList();

            Assert.Equal(expectedList, resultList);
        }

        [Fact] 
        private void NoFridgeProductsAndNoMatchingRecipes()
        {
            setUpMocks();

            //set up collections
            fridgeProductForDbSetList = new List<FridgeProduct> { };

            recipe1.RecipeProducts.Add(recipe1Product1);
            recipe2.RecipeProducts.Add(recipe2Product2);
            recipe3.RecipeProducts.Add(recipe3Product3);
            recipe4.RecipeProducts.Add(recipe4Product4);
            recipeProductForDbSetList = new List<RecipeProduct> { recipe1Product1, recipe2Product2,
                recipe3Product3, recipe4Product4};
            recipeForDbSetList = new List<Recipe> { recipe1, recipe2, recipe3, recipe4 };
            //fillDbSets
            setUpDbSets();

            List<Recipe> expectedList = new List<Recipe> { };

            ResultDataService resultDataService = new ResultDataService(dbContextCreator);
            List<Recipe> resultList = resultDataService.GetRecipes().ToList();

            Assert.Equal(expectedList, resultList);
        }

        [Fact]
        private void NoRecipeProductsAnywhereAllMatches()
        {
            setUpMocks();

            //set up collections
            fridgeProductForDbSetList = new List<FridgeProduct> { };
            recipe1.RecipeProducts.Add(recipe1Product1);
            recipe2.RecipeProducts.Add(recipe2Product2);
            recipe3.RecipeProducts.Add(recipe3Product3);
            recipe4.RecipeProducts.Add(recipe4Product4);
            recipeProductForDbSetList = new List<RecipeProduct> {
                recipe1Product1,
                recipe2Product2,
                recipe3Product3,
                recipe4Product4
            };
            recipeForDbSetList = new List<Recipe> { recipe1, recipe2, recipe3, recipe4 };
            //fillDbSets
            setUpDbSets();

            List<Recipe> expectedList = new List<Recipe> { };

            ResultDataService resultDataService = new ResultDataService(dbContextCreator);
            List<Recipe> resultList = resultDataService.GetRecipes().ToList();

            Assert.Equal(expectedList, resultList);
        }

        [Fact]
        private void NoMatchesWithSomeFridgeProducts()
        {
            setUpMocks();

            //set up collections
            fridge.FridgeProducts.Add(fridgeProduct1);
            fridge.FridgeProducts.Add(fridgeProduct2);
            fridge.FridgeProducts.Add(fridgeProduct3);
            fridgeProductForDbSetList = new List<FridgeProduct> {
                fridgeProduct1,
                fridgeProduct2,
                fridgeProduct3
            };
            recipe1.RecipeProducts.Add(recipe1Product4);
            recipe2.RecipeProducts.Add(recipe2Product4);
            recipe3.RecipeProducts.Add(recipe3Product4);
            recipe4.RecipeProducts.Add(recipe4Product4);
            recipeForDbSetList = new List<Recipe> { recipe1, recipe2, recipe3, recipe4 };
            recipeProductForDbSetList = new List<RecipeProduct> { recipe1Product4, recipe2Product4,
                recipe3Product4, recipe4Product4};
            //fillDbSets
            setUpDbSets();

            List<Recipe> expectedList = new List<Recipe> { };

            ResultDataService resultDataService = new ResultDataService(dbContextCreator);
            List<Recipe> resultList = resultDataService.GetRecipes().ToList();

            Assert.Equal(expectedList, resultList);
        }
    }
}
