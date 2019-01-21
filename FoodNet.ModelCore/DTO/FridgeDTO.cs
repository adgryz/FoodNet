using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.DTO
{
    public class FridgeDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<FridgeProductDTO> FridgeProducts { get; set; }
    }
}
