using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Command.Inventory
{
    public class AllocateProduct
    {
        public List<StockItem> Items { get; set; }
    }
}
