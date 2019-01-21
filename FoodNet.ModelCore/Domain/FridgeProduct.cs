using System;

namespace FoodNet.ModelCore.Domain
{
    public class FridgeProduct
    {
        public Guid Id { get; set; }

        public Guid FridgeId { get; set; }
        public Fridge Fridge { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
