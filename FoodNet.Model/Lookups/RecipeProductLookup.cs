using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodNet.Model.Lookups
{
    public class RecipeProductLookup
    {
        public Guid ProductId { get; set; }
        public Guid RecipeProductId { get; set; }
        public string ProductName { get; set; }
    }
}
