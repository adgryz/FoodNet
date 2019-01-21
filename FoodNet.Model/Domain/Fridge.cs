using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodNet.Model.Domain
{
    public class Fridge
    {
        public Guid Id { get; set; }

        public List<FridgeProduct> FridgeProducts { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
