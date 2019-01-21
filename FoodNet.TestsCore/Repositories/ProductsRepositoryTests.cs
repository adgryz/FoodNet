using AutoMapper;
using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNET.WebAPI.Data;
using FoodNET.WebAPI.Data.DTOFacade;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Linq;

namespace FoodNet.TestsCore
{
    public class ProductsRepositoryTests
    {

        private Fridge fridge1;
        private Fridge fridge2;

        private User user1;
        private User user2;

        private Guid firstUserGuid = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD");
        private Guid secondUserGuid = Guid.NewGuid();

        // productCategory 1, recipe 1, user1
        private Product product1;
        private Product product2;
        private Product product3;
        private FridgeProduct fridge1Product1;
        private FridgeProduct fridge1Product2;
        private FridgeProduct fridge1Product3;
        private RecipeProduct recipe1Product1;
        private RecipeProduct recipe1Product2;
        private RecipeProduct recipe1Product3;
        // productCategory 2, recipe 2, user2
        private Product product4;
        private Product product5;
        private Product product6;
        private FridgeProduct fridge1Product4;
        private FridgeProduct fridge1Product5;
        private FridgeProduct fridge1Product6;
        private RecipeProduct recipe2Product4;
        private RecipeProduct recipe2Product5;
        private RecipeProduct recipe2Product6;

        private Product newProduct1; // user1, category1
        private Product newProduct2; // user2, category2
        private Product newProduct3; // user2, category2

        private Recipe recipe1;
        private Recipe recipe2;

        private ProductCategory productCategory1;
        private ProductCategory productCategory2;
        private ProductCategory productCategory3;

        private FoodNetDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<FoodNetDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var ctx = new FoodNetDbContext(options);

            user1 = new User
            {
                Id = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD"),
                Name = "TestUser1",
                Email = "testmail1@gmail.com"
            };
            fridge1 = new Fridge
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD")
            };

            user2 = new User
            {
                Id = secondUserGuid,
                Name = "TestUser2",
                Email = "testmail2@gmail.com"
            };
            fridge2 = new Fridge
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("B71DBC28-AAAA-47B1-8A2A-C6A5BFCD78AD")
            };

            productCategory1 = new ProductCategory
            {
                Id = Guid.Parse("A5ADA8E6-04E1-49CA-A701-1265E216D69A"),
                Name = "Product Category 1"
            };
            productCategory2 = new ProductCategory
            {
                Id = Guid.Parse("94CAE204-3337-43FA-8C0E-24C927BACAC4"),
                Name = "Product Category 2"
            };
            productCategory3 = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Product Category 3"
            };

            product1 = new BasicProduct
            {
                Id = Guid.Parse("9C750539-3CA6-4239-941F-805B81C38CD4"),
                Name = "Salmon",
                Description = "",
                ProductCategoryId = productCategory1.Id,
                UserId = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD")
            };
            product2 = new BasicProduct
            {
                Id = Guid.Parse("9D8FAC6F-C194-44E5-A6D5-B6F6DBDEBBD0"),
                Name = "Mustard",
                Description = "",
                ProductCategoryId = productCategory1.Id,
                UserId = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD")
            };
            product3 = new BasicProduct
            {
                Id = Guid.Parse("F1F5610D-F065-46D8-9208-D7D1A0CB8C27"),
                Name = "Brown Sugar",
                Description = "",
                ProductCategoryId = productCategory1.Id,
                UserId = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD")
            };

            product4 = new BasicProduct
            {
                Id = Guid.Parse("06BBED64-A505-492C-889B-472F7DAB0FAA"),
                Name = "Butter",
                Description = "",
                ProductCategoryId = productCategory2.Id,
                UserId = secondUserGuid
            };
            product5 = new BasicProduct
            {
                Id = Guid.Parse("270B1180-45C6-4EE1-BC06-4819743859DC"),
                Name = "Cream",
                Description = "",
                ProductCategoryId = productCategory2.Id,
                UserId = secondUserGuid
            };
            product6 = new BasicProduct
            {
                Id = Guid.Parse("002D754C-176B-4324-A42E-2DC32369074D"),
                Name = "Eggs",
                Description = "",
                ProductCategoryId = productCategory2.Id,
                UserId = secondUserGuid
            };

            newProduct1 = new NewProduct
            {
                Id = Guid.NewGuid(),
                Name = "New product 1",
                Description = "",
                ProductCategoryId = productCategory1.Id,
                UserId = firstUserGuid
            };
            newProduct2 = new NewProduct
            {
                Id = Guid.NewGuid(),
                Name = "New product 2",
                Description = "",
                ProductCategoryId = productCategory2.Id,
                UserId = secondUserGuid
            };
            newProduct3 = new NewProduct
            {
                Id = Guid.NewGuid(),
                Name = "New product 3",
                Description = "",
                ProductCategoryId = productCategory2.Id,
                UserId = secondUserGuid
            };

            recipe1 = new Recipe
            {
                Id = Guid.Parse("83BD2A25-83EA-47D0-9B7B-0E4D528CF8C2"),
                Description = "",
                Title = "Salmon recipe",
                UserId = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD")
            };
            recipe2 = new Recipe
            {
                Id = Guid.Parse("F795B317-DB3B-469F-9891-62C5CCC9DF5D"),
                Description = "",
                Title = "Scrambled eggs",
                UserId = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD")
            };

            recipe1Product1 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe1.Id,
                ProductId = product1.Id
            };
            recipe1Product2 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe1.Id,
                ProductId = product2.Id
            };
            recipe1Product3 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe1.Id,
                ProductId = product3.Id
            };
            recipe2Product4 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe2.Id,
                ProductId = product4.Id
            };
            recipe2Product5 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe2.Id,
                ProductId = product5.Id
            };
            recipe2Product6 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe2.Id,
                ProductId = product6.Id
            };

            fridge1Product1 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge1.Id,
                ProductId = product1.Id
            };
            fridge1Product2 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge1.Id,
                ProductId = product2.Id
            };
            fridge1Product3 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge1.Id,
                ProductId = product3.Id
            };
            fridge1Product4 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge1.Id,
                ProductId = product4.Id
            };
            fridge1Product5 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge1.Id,
                ProductId = product5.Id
            };
            fridge1Product6 = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = fridge1.Id,
                ProductId = product6.Id
            };

            ctx.ProductCategories.Add(productCategory1);
            ctx.ProductCategories.Add(productCategory2);
            ctx.ProductCategories.Add(productCategory3);

            ctx.Products.Add(product1);
            ctx.Products.Add(product2);
            ctx.Products.Add(product3);
            ctx.Products.Add(product4);
            ctx.Products.Add(product5);
            ctx.Products.Add(product6);

            ctx.Products.Add(newProduct1);
            ctx.Products.Add(newProduct2);
            ctx.Products.Add(newProduct3);

            ctx.Recipes.Add(recipe1);
            ctx.Recipes.Add(recipe2);

            ctx.RecipeProducts.Add(recipe1Product1);
            ctx.RecipeProducts.Add(recipe1Product2);
            ctx.RecipeProducts.Add(recipe1Product3);
            ctx.RecipeProducts.Add(recipe2Product4);
            ctx.RecipeProducts.Add(recipe2Product5);
            ctx.RecipeProducts.Add(recipe2Product6);

            ctx.FridgeProducts.Add(fridge1Product1);
            ctx.FridgeProducts.Add(fridge1Product2);
            ctx.FridgeProducts.Add(fridge1Product3);
            ctx.FridgeProducts.Add(fridge1Product4);
            ctx.FridgeProducts.Add(fridge1Product5);
            ctx.FridgeProducts.Add(fridge1Product6);

            ctx.Users.Add(user1);
            ctx.Users.Add(user2);

            ctx.Fridges.Add(fridge1);
            ctx.Fridges.Add(fridge2);

            ctx.SaveChanges();

            return ctx;
        }

        private IMapper GetMapper()
        {            
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }

        // category tests
        [Fact]
        public void Returns4ProductsInFirstCategory()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(4, productsRepository.GetAllCategoryProducts(productCategory1.Id).Count());
        }
        [Fact]
        public void Returns1NewProductInFirstCategory()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(1, productsRepository.GetAllNewCategoryProducts(productCategory1.Id).Count());
        }
        [Fact]
        public void Returns5ProductsInSecondCategory()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(5, productsRepository.GetAllCategoryProducts(productCategory2.Id).Count());
        }
        [Fact]
        public void Returns2NewProductInSecondCategory()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(2, productsRepository.GetAllNewCategoryProducts(productCategory2.Id).Count());
        }
        [Fact]
        public void Returns0ProductsInThirdCategory()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(0, productsRepository.GetAllCategoryProducts(productCategory3.Id).Count());
        }

        // category tests with user distinction
        [Fact]
        public void Returns4ProductsFirstCategoryFirstUser()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(4, productsRepository.GetAllCategoryProductsForUser(productCategory1.Id, firstUserGuid).Count());
        }
        [Fact]
        public void Returns5ProductsSecondCategorySecondUser()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(5, productsRepository.GetAllCategoryProductsForUser(productCategory2.Id, secondUserGuid).Count());
        }
        [Fact]
        public void Returns3ProductsFirstCategorySecondUser()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(3, productsRepository.GetAllCategoryProductsForUser(productCategory1.Id, secondUserGuid).Count());
        }
        [Fact]
        public void Returns3ProductsSecondCategoryFirstUser()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(3, productsRepository.GetAllCategoryProductsForUser(productCategory2.Id, firstUserGuid).Count());
        }

        // user products visibility tests
        [Fact]
        public void Returns9AllProducts()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());
            
            Assert.Equal(9, productsRepository.GetAllProducts().Count());
        }
        [Fact]
        public void Returns7AllProductsForFirstUser()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(7, productsRepository.GetAllProductsForUser(firstUserGuid).Count());
        }
        [Fact]
        public void Returns8AllProductsForSecondUser()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            Assert.Equal(8, productsRepository.GetAllProductsForUser(secondUserGuid).Count());
        }

        // adding products
        [Fact]
        public void WontAddBasicProductWithSameName()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            int productsCountBefore = ctx.Products.Count();
            BasicProduct basicProduct = new BasicProduct
            {
                Id = Guid.NewGuid(),
                Name = product1.Name,
                Description = "",
                ProductCategoryId = productCategory3.Id,
                UserId = secondUserGuid
            };
            productsRepository.AddBasicProduct(basicProduct);
            int productsCountAfter = ctx.Products.Count();
            Assert.Equal(productsCountBefore, productsCountAfter);
        }
        [Fact]
        public void WontAddNewProductToBasicProductAfter9Times()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            int basicProductsCountBefore = ctx.BasicProducts.Count();

            for (int i = 0; i < 10; i++)
            {
                NewProduct newProduct = new NewProduct
                {
                    Id = Guid.NewGuid(),
                    Name = "NAME",
                    Description = "",
                    ProductCategoryId = productCategory3.Id,
                    UserId = secondUserGuid
                };
                productsRepository.AddNewProduct(newProduct);
            }

            int basicProductsCountAfter = ctx.BasicProducts.Count();
            Assert.Equal(basicProductsCountBefore, basicProductsCountAfter);
        }
        [Fact]
        public void AddsNewProductToBasicProductsAfter10Times()
        {
            FoodNetDbContext ctx = GetContextWithData();
            ProductsRepository productsRepository = new ProductsRepository(ctx, GetMapper());

            int basicProductsCountBefore = ctx.BasicProducts.Count();
            for (int i = 0; i <= 10; i++)
            {
                NewProduct newProduct = new NewProduct
                {
                    Id = Guid.NewGuid(),
                    Name = "NAME",
                    Description = "",
                    ProductCategoryId = productCategory3.Id,
                    UserId = secondUserGuid
                };
                productsRepository.AddNewProduct(newProduct);
            }

            int basicProductsCountAfter = ctx.BasicProducts.Count();
            Assert.Equal(basicProductsCountBefore + 1, basicProductsCountAfter);
        }
    }
}
