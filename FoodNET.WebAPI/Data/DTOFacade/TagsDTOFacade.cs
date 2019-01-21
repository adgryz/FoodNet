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
    public class TagsDTOFacade
    {
        private ITagsRepository _tagsRepository;
        private IMapper _mapper;

        public TagsDTOFacade(ITagsRepository tagsRepository, IMapper mapper)
        {
            _tagsRepository = tagsRepository;
            _mapper = mapper;
        }
        public IEnumerable<TagDTO> GetAllTags()
        {
            return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(_tagsRepository.GetAllTags());
        }
    }
}
