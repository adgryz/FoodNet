using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodNet.Model.Domain
{
    public class ProductCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
