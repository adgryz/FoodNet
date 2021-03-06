﻿using AutoMapper;
using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNET.WebAPI.Data;
using FoodNET.WebAPI.Data.DTOFacade;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;

namespace FoodNet.TestsCore.Repositories
{
    public class RecipesRepositoryTests
    {

        // all products tests
        [Fact]
        public void ReturnsAllRecipesWithNoTags()
        {
            var ctx = GetContextWithData();
            RecipesRepository recipesRepository = new RecipesRepository(ctx);

            List<Recipe> expectedList = ctx.Recipes.ToList();
            SortByRecipeId(expectedList);

            List<Recipe> actualList = recipesRepository.GetAllPublicPlusUserRecipes(firstUserGuid).ToList();
            SortByRecipeId(actualList);

            actualList.ShouldBeEquivalentTo(expectedList);
        }
        [Fact]
        public void ReturnsNoRecipesWithUnmatchedTag()
        {
            var ctx = GetContextWithData();
            RecipesRepository recipesRepository = new RecipesRepository(ctx);
            Assert.Equal(0, recipesRepository
                .GetAllPublicPlusUserRecipes(firstUserGuid, new List <Guid> { tag4.Id }).Count());
        }
        [Fact]
        public void ReturnsAllRecipesWithCommonTag()
        {
            var ctx = GetContextWithData();
            RecipesRepository recipesRepository = new RecipesRepository(ctx);

            List<Recipe> expectedList = new List<Recipe> { recipe1, recipe2, recipe3 };
            SortByRecipeId(expectedList);

            List<Recipe> actualList = recipesRepository.GetAllPublicPlusUserRecipes(firstUserGuid,
                new List <Guid> { tag2.Id }).ToList();
            SortByRecipeId(actualList);

            actualList.ShouldBeEquivalentTo(expectedList);
        }
        [Fact]
        public void ReturnsOnlyOneRecipeWithFirstTag()
        {
            var ctx = GetContextWithData();
            RecipesRepository recipesRepository = new RecipesRepository(ctx);
            List<Recipe> expectedList = new List<Recipe> { recipe1 };

            recipesRepository.GetAllPublicPlusUserRecipes(firstUserGuid,
                new List <Guid> { tag1.Id }).ShouldBeEquivalentTo(expectedList);
        }
        // user
        [Fact]
        public void ReturnsAllUserRecipesWithNoTags()
        {
            var ctx = GetContextWithData();
            RecipesRepository recipesRepository = new RecipesRepository(ctx);
            List<Recipe> expectedList = new List<Recipe> { recipe2 };
            recipesRepository.GetAllUserRecipes(secondUserGuid).ShouldBeEquivalentTo(expectedList);
        }
        [Fact]
        public void ReturnsNoUserRecipesWithUnmatchedTag()
        {
            var ctx = GetContextWithData();
            RecipesRepository recipesRepository = new RecipesRepository(ctx);
            Assert.Equal(0, recipesRepository.GetAllUserRecipes(secondUserGuid, new List<Guid> { tag4.Id }).Count());
        }
        [Fact]
        public void ReturnsAllUserRecipesWithTag()
        {
            var ctx = GetContextWithData();
            RecipesRepository recipesRepository = new RecipesRepository(ctx);

            List<Recipe> expectedList = new List<Recipe> { recipe1, recipe3 };
            SortByRecipeId(expectedList);

            List<Recipe> actualList = recipesRepository
                .GetAllUserRecipes(firstUserGuid, new List<Guid> { tag2.Id }).ToList();
            SortByRecipeId(actualList);

            actualList.ShouldBeEquivalentTo(expectedList);
        }
        [Fact]
        public void ReturnsOnlyOneUserRecipeWithFirstTag()
        {
            var ctx = GetContextWithData();
            RecipesRepository recipesRepository = new RecipesRepository(ctx);
            List<Recipe> expectedList = new List<Recipe> { recipe1 };

            recipesRepository.GetAllUserRecipes(firstUserGuid, new List<Guid> { tag1.Id })
                .ShouldBeEquivalentTo(expectedList);
        }

        private void SortByRecipeId(List<Recipe> list)
        {
            list.Sort((r1, r2) => r1.Id.ToString().CompareTo(r2.Id.ToString()));
        }

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
        private Product product6; // + recipe3
        private FridgeProduct fridge1Product4;
        private FridgeProduct fridge1Product5;
        private FridgeProduct fridge1Product6;
        private RecipeProduct recipe2Product4;
        private RecipeProduct recipe2Product5;
        private RecipeProduct recipe2Product6;
        private RecipeProduct recipe3Product6;
        

        private Product newProduct1; // user1, category1
        private Product newProduct2; // user2, category2
        private Product newProduct3; // user2, category2

        private Recipe recipe1;
        private Recipe recipe2;
        private Recipe recipe3;

        private ProductCategory productCategory1;
        private ProductCategory productCategory2;
        private ProductCategory productCategory3;

        private Tag tag1; // recipe1
        private Tag tag2; // recipe1, recipe3
        private Tag tag3; // recipe2
        private Tag tag4;

        private RecipeTag recipe1tag1;
        private RecipeTag recipe1tag2;
        private RecipeTag recipe2tag2;
        private RecipeTag recipe2tag3;
        private RecipeTag recipe3tag2;

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
                UserId = firstUserGuid
            };
            recipe2 = new Recipe
            {
                Id = Guid.Parse("F795B317-DB3B-469F-9891-62C5CCC9DF5D"),
                Description = "",
                Title = "Scrambled eggs",
                UserId = secondUserGuid
            };
            recipe3 = new Recipe
            {
                Id = Guid.NewGuid(),
                Description = "",
                Title = "Recipe 3",
                UserId = firstUserGuid
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
            recipe3Product6 = new RecipeProduct
            {
                Id = Guid.NewGuid(),
                RecipeId = recipe3.Id,
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

            tag1 = new Tag
            {
                Id = Guid.NewGuid(),
                Text = "tag1",
                RecipeTags = new List<RecipeTag>()
            };
            tag2 = new Tag
            {
                Id = Guid.NewGuid(),
                Text = "tag2",
                RecipeTags = new List<RecipeTag>()
            };
            tag3 = new Tag
            {
                Id = Guid.NewGuid(),
                Text = "tag3",
                RecipeTags = new List<RecipeTag>()
            };
            tag4 = new Tag
            {
                Id = Guid.NewGuid(),
                Text = "tag4",
                RecipeTags = new List<RecipeTag>()
            };

            recipe1tag1 = new RecipeTag
            {
                Id = Guid.NewGuid(),
                TagId = tag1.Id,
                RecipeId = recipe1.Id
            };
            recipe1tag2 = new RecipeTag
            {
                Id = Guid.NewGuid(),
                TagId = tag2.Id,
                RecipeId = recipe1.Id
            };
            recipe2tag2 = new RecipeTag
            {
                Id = Guid.NewGuid(),
                TagId = tag2.Id,
                RecipeId = recipe2.Id
            };
            recipe2tag3 = new RecipeTag
            {
                Id = Guid.NewGuid(),
                TagId = tag1.Id,
                RecipeId = recipe1.Id
            };
            recipe3tag2 = new RecipeTag
            {
                Id = Guid.NewGuid(),
                TagId = tag2.Id,
                RecipeId = recipe3.Id
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
            ctx.Recipes.Add(recipe3);

            ctx.RecipeProducts.Add(recipe1Product1);
            ctx.RecipeProducts.Add(recipe1Product2);
            ctx.RecipeProducts.Add(recipe1Product3);
            ctx.RecipeProducts.Add(recipe2Product4);
            ctx.RecipeProducts.Add(recipe2Product5);
            ctx.RecipeProducts.Add(recipe2Product6);
            ctx.RecipeProducts.Add(recipe3Product6);

            ctx.FridgeProducts.Add(fridge1Product1);
            ctx.FridgeProducts.Add(fridge1Product2);
            ctx.FridgeProducts.Add(fridge1Product3);
            ctx.FridgeProducts.Add(fridge1Product4);
            ctx.FridgeProducts.Add(fridge1Product5);
            ctx.FridgeProducts.Add(fridge1Product6);

            ctx.Tags.Add(tag1);
            ctx.Tags.Add(tag2);
            ctx.Tags.Add(tag3);
            ctx.Tags.Add(tag4);

            ctx.RecipeTags.Add(recipe1tag1);
            ctx.RecipeTags.Add(recipe1tag2);
            ctx.RecipeTags.Add(recipe2tag2);
            ctx.RecipeTags.Add(recipe2tag3);
            ctx.RecipeTags.Add(recipe3tag2);

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
    }
}
