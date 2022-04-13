using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Event.User
{
    public class UserValidate
    {
        public List<string> Error { get; set; }
        public bool IsValid { get; set; }
    }
}
