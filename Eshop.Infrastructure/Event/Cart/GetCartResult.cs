using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Event.Cart
{
    public class GetCartResult
    {
        public string CartId { get; set; }
        public decimal Amount { get; set; }
        public List<CartItemCreated> Items { get; set; }
        public string UserId { get; set; }
    }
}
