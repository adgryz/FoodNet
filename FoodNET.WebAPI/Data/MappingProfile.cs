using AutoMapper;
using FoodNet.ModelCore.Domain;
using FoodNet.ModelCore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNET.WebAPI.Data.DTOFacade
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore());
            CreateMap<RecipeDTO, Recipe>()
                .ForMember(dest => dest.RecipeProducts, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeTags, opt => opt.Ignore());

            CreateMap<RecipeUpdateDTO, Recipe>()
                .ForMember(dest => dest.RecipeProducts, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeTags, opt => opt.Ignore());
            CreateMap<Recipe, RecipeUpdateDTO>()
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore());

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Fridge, FridgeDTO>();
            CreateMap<FridgeDTO, Fridge>();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.ProductCategoryId));
            CreateMap<ProductDTO, Product>();

            CreateMap<ProductDTO, BasicProduct>();
            CreateMap<ProductDTO, NewProduct>();

            CreateMap<BasicProduct, NewProduct>();
            CreateMap<NewProduct, BasicProduct>();

            CreateMap<RecipeProduct, RecipeProductDTO>();
            CreateMap<RecipeProductDTO, RecipeProduct>();

            CreateMap<FridgeProduct, FridgeProductDTO>();
            CreateMap<FridgeProductDTO, FridgeProduct>();

            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<ProductCategoryDTO, ProductCategory>();

            CreateMap<RecipeTagDTO, RecipeTag>();
            CreateMap<RecipeTag, RecipeTagDTO>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Tag.Text));

            CreateMap<TagDTO, Tag>();
            CreateMap<Tag, TagDTO>();
        }
    }
}
