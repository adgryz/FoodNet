using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.DTO
{
    public class RecipeProductDTO
    {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public Guid ProductId { get; set; }
    }
}
