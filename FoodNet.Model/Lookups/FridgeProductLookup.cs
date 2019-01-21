using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodNet.Model.Lookups
{
    public class FridgeProductLookup
    {
        public Guid ProductId { get; set; }
        public Guid FridgeProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
