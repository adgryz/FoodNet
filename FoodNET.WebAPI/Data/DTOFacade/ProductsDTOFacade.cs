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
    public class ProductsDTOFacade
    {
        private IProductsRepository _productsRepository;
        private IPriorityUserProductsRepository _priorityUserProductsRepository;
        private IMapper _mapper;

        public ProductsDTOFacade(IProductsRepository productsRepository, IMapper mapper, IPriorityUserProductsRepository priorityUserProductsRepository)
        {
            _productsRepository = productsRepository;
            _priorityUserProductsRepository = priorityUserProductsRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductDTO> GetAllProductsForUser(Guid UserId)
        {
            IEnumerable<Product> userProducts = _productsRepository.GetAllProductsForUser(UserId);
            IEnumerable<Product> sortedUserProducts = userProducts
                .GroupJoin(
                  _priorityUserProductsRepository.GetAllPriorityUserProducts().Where(pup => pup.UserId == UserId),
                  up => up.Id,
                  pup => pup.ProductId,
                  (up, pup) => new { Product = up, Count = pup.FirstOrDefault() == null ? 0 : pup.FirstOrDefault().Count})
            .OrderByDescending(x => x.Count)
            .Select(x => x.Product)
            .ToList();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(sortedUserProducts);
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_productsRepository.GetAllProducts());
        }

        public IEnumerable<ProductDTO> GetAllProductsForCategory(Guid categoryId)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_productsRepository.GetAllCategoryProducts(categoryId));
        }

        public IEnumerable<ProductCategoryDTO> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryDTO>>(_productsRepository.GetAllCategories());
        }

        // temp
        public IEnumerable<ProductDTO> GetAllBasicProducts()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>> (_productsRepository.GetAllBasicProducts());
        }

        // temp
        public IEnumerable<ProductDTO> GetAllNewProducts()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_productsRepository.GetAllNewProducts());
        }

        public void AddNewProduct(ProductDTO product, Guid userId)
        {
            NewProduct newProduct = new NewProduct
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                ProductCategoryId = product.CategoryId,
                UserId = userId
            };
            _productsRepository.AddNewProduct(newProduct);
            _productsRepository.Save();
        }
    }
}
