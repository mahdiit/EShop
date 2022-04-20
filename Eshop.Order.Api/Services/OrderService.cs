using Eshop.Infrastructure.Command.Order;
using Eshop.Infrastructure.Event.Order;
using Eshop.Order.Api.Repositories;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Services
{
    public class OrderService: IOrderService
    {
        IOrderRepository orderRepository;
        public OrderService(IOrderRepository repository)
        {
            orderRepository = repository;
        }

        public async Task<GetOrderResult> GetOrder(GetOrder order)
        {
            return await orderRepository.GetOrder(order);
        }
    }
}
