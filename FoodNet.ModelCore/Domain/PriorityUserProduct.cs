using System;
using System.Collections.Generic;
using System.Text;

namespace FoodNet.ModelCore.Domain
{
    public class PriorityUserProduct
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
