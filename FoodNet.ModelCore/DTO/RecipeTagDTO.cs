using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.DTO
{
    public class RecipeTagDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid RecipeId { get; set; }
        public Guid TagId { get; set; }
    }
}
