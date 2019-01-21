using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.Domain
{
    public class RecipeTag
    {
        public Guid Id { get; set; }

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
