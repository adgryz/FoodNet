using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodNet.ModelCore.Domain
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Fridge> Fridges { get; set; }
        public List<PriorityUserProduct> PriorityUserProducts { get; set; }
    }
}
