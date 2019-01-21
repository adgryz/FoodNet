using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodNet.Model.Domain
{
    public class RecipeProduct
    {
        public Guid Id { get; set; }

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
