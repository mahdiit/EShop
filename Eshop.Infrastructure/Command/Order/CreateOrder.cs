﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Command.Order
{
    public class CreateOrder
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
    }
}
