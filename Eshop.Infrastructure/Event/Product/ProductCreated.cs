using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Command.Product
{
    public class ProductCreated
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
