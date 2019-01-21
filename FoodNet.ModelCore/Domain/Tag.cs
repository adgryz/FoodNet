using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Text { get; set;  }

        public List<RecipeTag> RecipeTags { get; set; }
    }
}
