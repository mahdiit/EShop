using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Command.Cart
{
    public class GetCartResult
    {
        public GetCartResult()
        {
            Items = new List<CartItemCreated>();
        }
        public string CartId { get; set; }
        public decimal Amount { get; set; }
        public IList<CartItemCreated> Items { get; set; }
        public string UserId { get; set; }
    }
}
