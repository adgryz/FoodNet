﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
