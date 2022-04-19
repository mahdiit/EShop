using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Command.Cart
{
    public class RemoveCartItem
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
    }
}
