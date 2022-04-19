using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Command.Cart
{
    public class AddCartItem
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
