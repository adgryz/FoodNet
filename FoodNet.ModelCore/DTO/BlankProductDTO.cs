using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.DTO
{
    public class BlankProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
}
