using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Command.Wallet
{
    public class DeductFunds
    {
        public string UserId { get; set; }
        public decimal DebitAmount { get; set; }
    }
}
