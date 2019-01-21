using AutoMapper;
using FoodNet.ModelCore.Domain;
using FoodNet.ModelCore.DTO;
using FoodNET.WebAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNET.WebAPI.Data.DTOFacade
{
    public class RecipesDTOFacade
    {
        private IRecipesRepository _recipesRepository;
        private IProductsRepository _productsRepository;
        private ITagsRepository _tagsRepository;
        private IMapper _mapper;

        public RecipesDTOFacade(IRecipesRepository recipesRepository,
                                IProductsRepository productsRepository,
                                ITagsRepository tagsRepository,
                                IMapper mapper)
        {
            _recipesRepository = recipesRepository;
            _productsRepository = productsRepository;
            _tagsRepository = tagsRepository;
            _mapper = mapper;
        }
        public IEnumerable<RecipeDTO> GetAllPublicPlusUserRecipes(Guid userId, List<Guid> tags)
        {
            List<RecipeDTO> recipesDTOs = new List<RecipeDTO>();
            _recipesRepository.GetAllPublicPlusUserRecipes(userId, tags).ToList().ForEach(r =>
                recipesDTOs.Add(RecipeToRecipeDTO(r, userId)));
            return recipesDTOs;
        }
        public IEnumerable<RecipeDTO> GetAllUserRecipes(Guid userId, List<Guid> tags)
        {
            List<RecipeDTO> recipesDTOs = new List<RecipeDTO>();
            _recipesRepository.GetAllUserRecipes(userId, tags).ToList().ForEach(r =>
                recipesDTOs.Add(RecipeToRecipeDTO(r, userId)));
            return recipesDTOs;
        }
        private RecipeDTO RecipeToRecipeDTO(Recipe recipe, Guid userId)
        {
            RecipeDTO recipeDTO = _mapper.Map<Recipe, RecipeDTO>(recipe);

            recipeDTO.Products = new List<ProductDTO>();
            recipe.RecipeProducts.ForEach(rp =>
                recipeDTO.Products.Add(
                    _mapper.Map<Product, ProductDTO>(_productsRepository.GetProductById(rp.ProductId))));

            recipeDTO.Tags = new List<TagDTO>();
            recipe.RecipeTags.ForEach(rt =>
                recipeDTO.Tags.Add(
                    _mapper.Map<Tag, TagDTO>(_tagsRepository.GetTagById(rt.TagId))));

            recipeDTO.IsMy = recipe.UserId == userId;
            return recipeDTO;
        }

        public void UpdateRecipe(RecipeUpdateDTO updatedRecipe)
        {
            _recipesRepository.UpdateRecipeSimpleValues(_mapper.Map<RecipeUpdateDTO, Recipe>(updatedRecipe));

            Recipe recipe = _recipesRepository.GetRecipeById(updatedRecipe.Id);
            UpdateRecipeProducts(recipe, updatedRecipe);
            UpdateRecipeTags(recipe, updatedRecipe);
        }

        private void UpdateRecipeProducts(Recipe originalRecipe, RecipeUpdateDTO updatedRecipe)
        {
            List<Guid> productsToRemoveGuids = new List<Guid>();
            List<Guid> productsToAddGuids = new List<Guid>();
            if (updatedRecipe.Products != null)
            {
                updatedRecipe.Products.ForEach(p => {
                    if (!_recipesRepository.ContainsProduct(updatedRecipe.Id, p.Id))
                    {
                        productsToAddGuids.Add(p.Id);
                    }
                });
            }

            if(originalRecipe.RecipeProducts != null && updatedRecipe.Products != null)
            {
                originalRecipe.RecipeProducts.ForEach(rp =>
                {
                    if (!updatedRecipe.Products.Any(p => p.Id == rp.ProductId))
                    {
                        productsToRemoveGuids.Add(rp.ProductId);
                    }
                });
            }

            productsToRemoveGuids.ForEach(g => _recipesRepository.RemoveProductById(g));
            productsToAddGuids.ForEach(g => _recipesRepository.AddProduct(
                new RecipeProduct
                {
                    Id = Guid.NewGuid(),
                    ProductId = g,
                    RecipeId = updatedRecipe.Id
                }));
        }

        private void UpdateRecipeTags(Recipe originalRecipe, RecipeUpdateDTO updatedRecipe)
        {
            List<Guid> tagsToRemoveGuids = new List<Guid>();
            List<Guid> tagsToAddGuids = new List<Guid>();
            if (updatedRecipe.Tags != null)
            {
                updatedRecipe.Tags.ForEach(t => {
                    if (!_recipesRepository.ContainsTag(updatedRecipe.Id, t.Id))
                    {
                        tagsToAddGuids.Add(t.Id);
                    }
                });
            }

            if (originalRecipe.RecipeTags != null && updatedRecipe.Tags != null)
            {
                originalRecipe.RecipeTags.ForEach(rt =>
                {
                    if (!updatedRecipe.Tags.Any(t => t.Id == rt.TagId))
                    {
                        tagsToRemoveGuids.Add(rt.TagId);
                    }
                });
            }

            tagsToRemoveGuids.ForEach(g => _recipesRepository.RemoveTagById(g));
            tagsToAddGuids.ForEach(g => _recipesRepository.AddTag(
                new RecipeTag
                {
                    Id = Guid.NewGuid(),
                    TagId = g,
                    RecipeId = updatedRecipe.Id  
                }));
        }

        public void AddRecipe(RecipeUpdateDTO newRecipe)
        {
            Recipe recipe = _mapper.Map<RecipeUpdateDTO, Recipe>(newRecipe);
            _recipesRepository.AddRecipe(recipe);
            UpdateRecipeProducts(recipe, newRecipe);
            UpdateRecipeTags(recipe, newRecipe);
        }
    }
}
