using AutoMapper;
using FoodNET.WebAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodNET.WebAPI.Data.DTOFacade
{
    public class UsersDTOFacade
    {
        private IUsersRepository _usersRepository;
        private IMapper _mapper;

        public UsersDTOFacade(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }
    }
}
