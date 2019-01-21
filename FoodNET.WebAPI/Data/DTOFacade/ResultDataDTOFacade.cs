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
    public class ResultDataDTOFacade
    {
        private IResultDataService _resultDataService;
        private IProductsRepository _productsRepository;
        private ITagsRepository _tagsRepository;
        private IMapper _mapper;

        public ResultDataDTOFacade(IResultDataService resultDataService,
                                   IProductsRepository productsRepository,
                                   ITagsRepository tagsRepository,
                                   IMapper mapper)
        {
            _resultDataService = resultDataService;
            _productsRepository = productsRepository;
            _tagsRepository = tagsRepository;
            _mapper = mapper;
        }
        public IEnumerable<RecipeDTO> GetAllMatchingPublicPlusUserRecipes(Guid userId, List<Guid> tags)
        {
            List<RecipeDTO> recipesDTOs = new List<RecipeDTO>();
            _resultDataService.GetAllMatchingPublicPlusUserRecipes(userId, tags).ToList().ForEach(r =>
                recipesDTOs.Add(RecipeToRecipeDTO(r, userId)));
            return recipesDTOs;
        }
        public IEnumerable<RecipeDTO> GetAllUserMatchingRecipes(Guid userId, List<Guid> tags)
        {
            List<RecipeDTO> recipesDTOs = new List<RecipeDTO>();
            _resultDataService.GetMatchingUserRecipes(userId, tags).ToList().ForEach(r =>
                recipesDTOs.Add(RecipeToRecipeDTO(r, userId)));
            return recipesDTOs;
        }

        private RecipeDTO RecipeToRecipeDTO(Recipe recipe, Guid userId)
        {
            RecipeDTO recipeDTO = _mapper.Map<Recipe, RecipeDTO>(recipe);

            recipeDTO.Products = new List<ProductDTO>();
            recipe.RecipeProducts.ForEach(rp => recipeDTO.Products.Add(
                _mapper.Map<Product, ProductDTO>(_productsRepository.GetProductById(rp.ProductId))));

            recipeDTO.Tags = new List<TagDTO>();
            recipe.RecipeTags.ForEach(rt =>
                recipeDTO.Tags.Add(
                    _mapper.Map<Tag, TagDTO>(_tagsRepository.GetTagById(rt.TagId))));

            recipeDTO.IsMy = recipe.UserId == userId;
            return recipeDTO;
        }
    }
}
