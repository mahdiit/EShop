using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Event.Order
{
    public class GetAllOrderResult
    {
        public List<OrderCreated> Result { get; set; }
    }
}
