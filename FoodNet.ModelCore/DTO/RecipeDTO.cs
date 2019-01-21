using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.DTO
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<TagDTO> Tags { get; set; }
        public bool IsMy { get; set; }
        public bool IsPrivate { get; set; }
    }
}
