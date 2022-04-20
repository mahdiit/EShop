using Eshop.Infrastructure.Command.Order;
using Eshop.Infrastructure.Event.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Services
{
    public interface IOrderService
    {
        Task<GetOrderResult> GetOrder(GetOrder order);
        Task<GetAllOrderResult> GetAllOrder(GetAllOrder order);
    }
}
