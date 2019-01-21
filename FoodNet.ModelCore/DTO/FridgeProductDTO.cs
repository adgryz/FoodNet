using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.DTO
{
    public class FridgeProductDTO
    {
        public Guid Id { get; set; }
        public Guid FridgeId { get; set; }
        public Guid ProductId { get; set; }
    }
}
