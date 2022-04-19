using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Event.Cart
{
    public class CartItemCreated
    {
        public string ProductId { get; set; }
        public int Quanitty { get; set; }
        public decimal Price { get; set; }
    }
}
