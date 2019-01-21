using AutoMapper;
using FoodNet.DataAccessCore;
using FoodNet.ModelCore.Domain;
using FoodNET.WebAPI.Data;
using FoodNET.WebAPI.Data.DTOFacade;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Linq;
using FoodNET.WebAPI.Data.Interfaces;
using Moq;
using System.Collections.Generic;
using FoodNet.ModelCore.DTO;
using FluentAssertions;

namespace FoodNet.TestsCore.DTOFacades
{
    public class RecipesDTOFacadeTests
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

        private Tag tag1; // recipe1
        private Tag tag2; // recipe1
        private Tag tag3; // recipe2
        private List<Tag> tagsList;

        private RecipeTag recipe1tag1;
        private RecipeTag recipe1tag2;
        private RecipeTag recipe2tag3;

        private Recipe recipe1;
        private Recipe recipe2;
        private List<Recipe> recipesList;

        private ProductCategory productCategory1;
        private ProductCategory productCategory2;
        private ProductCategory productCategory3;

        private Mock<IRecipesRepository> recipesRepositoryMock;
        private Mock<IProductsRepository> poorProductsRepositoryMock;
        private Mock<ITagsRepository> poorTagsRepositoryMock;

        private List<Product> productsList;
        
        private Recipe tempUpdatingRecipe;

        private void InitFields()
        {

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
                Id = Guid.Parse("B71DBC28-6A02-47B1-8A2A-C6A5BFCD78AD"),
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

            recipe1tag1 = new RecipeTag
            {
                Id = Guid.NewGuid(),
                TagId = tag1.Id,
                RecipeId = recipe1.Id
            };
            recipe1tag2 = new RecipeTag
            {
                Id = Guid.NewGuid(),
                TagId = tag1.Id,
                RecipeId = recipe1.Id
            };
            recipe2tag3 = new RecipeTag
            {
                Id = Guid.NewGuid(),
                TagId = tag1.Id,
                RecipeId = recipe1.Id
            };

            recipe1.RecipeProducts = new List<RecipeProduct>
            {
                recipe1Product1,
                recipe1Product2,
                recipe1Product3
            };
            recipe1.RecipeTags = new List<RecipeTag> {
                recipe1tag1,
                recipe1tag2
            };

            recipe2.RecipeProducts = new List<RecipeProduct>();
            recipe2.RecipeTags = new List<RecipeTag>
            {
                recipe2tag3
            };

            tempUpdatingRecipe = new Recipe
            {
                RecipeProducts = new List<RecipeProduct>(),
                RecipeTags = new List<RecipeTag> { }
            };

            recipesList = new List<Recipe>
            {
                recipe1,
                recipe2
            };

            productsList = new List<Product> { product1, product2, product3, product4, product5, product6 };

            poorProductsRepositoryMock = new Mock<IProductsRepository>();
            poorProductsRepositoryMock.Setup(pr => pr.GetProductById(It.IsAny<Guid>()))
                .Returns<Guid>(g => productsList.Where(p => p.Id == g).FirstOrDefault());

            tagsList = new List<Tag> { tag1, tag2, tag3 };

            poorTagsRepositoryMock = new Mock<ITagsRepository>();
            poorTagsRepositoryMock.Setup(tr => tr.GetTagById(It.IsAny<Guid>()))
                .Returns<Guid>(g => tagsList.Where(t => t.Id == g).FirstOrDefault());

            recipesRepositoryMock = new Mock<IRecipesRepository>();
            recipesRepositoryMock.Setup(rr => rr.RemoveRecipeProduct(It.IsAny<RecipeProduct>()))
                .Callback<RecipeProduct>(rp =>
                {
                    tempUpdatingRecipe.RecipeProducts.Remove(rp);
                });
            recipesRepositoryMock.Setup(rr => rr.AddProduct(It.IsAny<RecipeProduct>()))
                .Callback<RecipeProduct>(rp =>
                {
                    tempUpdatingRecipe.RecipeProducts.Add(rp);
                });
            recipesRepositoryMock.Setup(rr => rr.RemoveProductById(It.IsAny<Guid>()))
               .Callback<Guid>(g =>
                {
                    tempUpdatingRecipe.RecipeProducts.Remove(
                        tempUpdatingRecipe.RecipeProducts.Where(r => r.ProductId == g).FirstOrDefault());
                });
            recipesRepositoryMock.Setup(rr => rr.RemoveRecipeTag(It.IsAny<RecipeTag>()))
                .Callback<RecipeTag>(rt =>
                {
                    tempUpdatingRecipe.RecipeTags.Remove(rt);
                });
            recipesRepositoryMock.Setup(rr => rr.RemoveTagById(It.IsAny<Guid>()))
                .Callback<Guid>(g =>
                {
                    tempUpdatingRecipe.RecipeTags.Remove(
                        tempUpdatingRecipe.RecipeTags.Where(rt => rt.TagId == g).FirstOrDefault());
                });
            recipesRepositoryMock.Setup(rr => rr.AddTag(It.IsAny<RecipeTag>()))
                .Callback<RecipeTag>(rt =>
                {
                    tempUpdatingRecipe.RecipeTags.Add(rt);
                });
            recipesRepositoryMock.Setup(rr => rr.GetRecipeById(It.IsAny<Guid>()))
                .Returns<Guid>(recipeId =>
                {
                    return tempUpdatingRecipe;
                });
            recipesRepositoryMock.Setup(rr => rr.ContainsProduct(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns<Guid, Guid>((recipeId, productId) =>
                {
                    return tempUpdatingRecipe.RecipeProducts.Any(rp => rp.ProductId == productId);
                });
            recipesRepositoryMock.Setup(rr => rr.ContainsTag(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns<Guid, Guid>((recipeId, tagId) =>
                {
                    return tempUpdatingRecipe.RecipeTags.Any(rt => rt.Id == tagId);
                });
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

        private RecipeUpdateDTO RecipeToRecipeUpdateDTO(Recipe recipe)
        {
            RecipeUpdateDTO recipeDTO = GetMapper().Map<Recipe, RecipeUpdateDTO>(recipe);

            recipeDTO.Products = new List<ProductDTO>();
            recipe.RecipeProducts.ForEach(rp =>
                recipeDTO.Products.Add(
            GetMapper().Map<Product, ProductDTO>(productsList.Where(p => p.Id == rp.ProductId).Single())));

            recipeDTO.Tags = new List<TagDTO>();
            recipe.RecipeTags.ForEach(rt =>
                recipeDTO.Tags.Add(
            GetMapper().Map<Tag, TagDTO>(tagsList.Where(t => t.Id == rt.TagId).Single())));

            return recipeDTO;
        }
        [Fact]
        public void UpdateRemovesRecipeProducts()
        {
            InitFields();
            RecipesDTOFacade recipesDTOFacade = new RecipesDTOFacade(
                recipesRepositoryMock.Object,
                poorProductsRepositoryMock.Object,
                poorTagsRepositoryMock.Object,
                GetMapper());

            tempUpdatingRecipe = recipe1.DeepCopy();
            List<RecipeProduct> recipeProducts = tempUpdatingRecipe.RecipeProducts.ToList();

            tempUpdatingRecipe.RecipeProducts = new List<RecipeProduct>();
            RecipeUpdateDTO recipeDTO = RecipeToRecipeUpdateDTO(tempUpdatingRecipe);
            tempUpdatingRecipe.RecipeProducts.AddRange(recipeProducts);

            recipesDTOFacade.UpdateRecipe(recipeDTO);
            
            Assert.Equal(0, tempUpdatingRecipe.RecipeProducts.Count());
        }

        [Fact]
        public void UpdateAddsRecipeProducts()
        {
            InitFields();
            RecipesDTOFacade recipesDTOFacade = new RecipesDTOFacade(
                recipesRepositoryMock.Object,
                poorProductsRepositoryMock.Object,
                poorTagsRepositoryMock.Object,
                GetMapper());

            tempUpdatingRecipe = recipe1.DeepCopy();
            RecipeUpdateDTO recipeDTO = RecipeToRecipeUpdateDTO(tempUpdatingRecipe);

            tempUpdatingRecipe.RecipeProducts = new List<RecipeProduct>();

            recipesDTOFacade.UpdateRecipe(recipeDTO);
            
            Assert.Equal(3, tempUpdatingRecipe.RecipeProducts.Count());
        }
        [Fact]
        public void UpdateRemovesRecipeTags()
        {
            InitFields();
            RecipesDTOFacade recipesDTOFacade = new RecipesDTOFacade(
                recipesRepositoryMock.Object,
                poorProductsRepositoryMock.Object,
                poorTagsRepositoryMock.Object,
                GetMapper());

            tempUpdatingRecipe = recipe1.DeepCopy();
            List<RecipeTag> recipeTags = tempUpdatingRecipe.RecipeTags.ToList();

            tempUpdatingRecipe.RecipeTags = new List<RecipeTag>();
            RecipeUpdateDTO recipeDTO = RecipeToRecipeUpdateDTO(tempUpdatingRecipe);
            tempUpdatingRecipe.RecipeTags.AddRange(recipeTags);

            recipesDTOFacade.UpdateRecipe(recipeDTO);
            
            Assert.Equal(0, tempUpdatingRecipe.RecipeTags.Count());
        }
        [Fact]
        public void UpdateAddsRecipeTags()
        {
            InitFields();
            RecipesDTOFacade recipesDTOFacade = new RecipesDTOFacade(
                recipesRepositoryMock.Object,
                poorProductsRepositoryMock.Object,
                poorTagsRepositoryMock.Object,
                GetMapper());

            tempUpdatingRecipe = recipe1.DeepCopy();
            RecipeUpdateDTO recipeDTO = RecipeToRecipeUpdateDTO(tempUpdatingRecipe);

            tempUpdatingRecipe.RecipeTags = new List<RecipeTag>();

            recipesDTOFacade.UpdateRecipe(recipeDTO);
            
            Assert.Equal(2, tempUpdatingRecipe.RecipeTags.Count());
        }
    }
}