using Eshop.Infrastructure.Command.Order;
using Eshop.Infrastructure.Event.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Repositories
{
    public interface IOrderRepository
    {
        Task<GetOrderResult> GetOrder(GetOrder order);
        Task<GetAllOrderResult> GetAllOrder(GetAllOrder order);
        Task<OrderCreated> CreateOrder(CreateOrder createOrder);
    }
}
