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
    public class FridgesDTOFacade
    {
        private IFridgeRepository _fridgeRepository;
        private IMapper _mapper;

        public FridgesDTOFacade(IFridgeRepository fridgeRepository, IMapper mapper)
        {
            _fridgeRepository = fridgeRepository;
            _mapper = mapper;
        }
        public FridgeDTO GetUserFridge(Guid UserId)
        {
            return _mapper.Map<Fridge, FridgeDTO>(_fridgeRepository.GetUserFridge(UserId));
        }
        public IEnumerable<ProductDTO> GetAllFridgeProducts(Guid userId)
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_fridgeRepository.GetAllFridgeProducts(userId));
        }
        public void AddProduct(Guid userId, Guid productId)
        {
            FridgeProduct fridgeProduct = new FridgeProduct
            {
                Id = Guid.NewGuid(),
                FridgeId = _fridgeRepository.GetUserFridge(userId).Id,
                ProductId = productId
            };
            _fridgeRepository.AddProduct(fridgeProduct);
        }
    }
}
