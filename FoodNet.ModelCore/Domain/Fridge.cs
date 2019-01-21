using System;
using System.Collections.Generic;

namespace FoodNet.ModelCore.Domain
{
    public class Fridge
    {
        public Guid Id { get; set; }

        public List<FridgeProduct> FridgeProducts { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
