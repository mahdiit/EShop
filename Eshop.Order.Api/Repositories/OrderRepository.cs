using Eshop.Infrastructure.Command.Order;
using Eshop.Infrastructure.Event.Order;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Order.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        IMongoDatabase database;
        IMongoCollection<OrderCreated> Table;
        public OrderRepository(IMongoDatabase mongo)
        {
            database = mongo;
            Table = database.GetCollection<OrderCreated>("order");
        }

        public async Task<GetAllOrderResult> GetAllOrder(GetAllOrder order)
        {
            var item = await Table.AsQueryable().Where(x => x.UserId == order.UserId).ToListAsync();
            if (item.Count > 0)
                return new GetAllOrderResult()
                {
                    Result = item
                };

            return new GetAllOrderResult() { Result = new List<OrderCreated>() };

        }

        public async Task<GetOrderResult> GetOrder(GetOrder order)
        {
            var item = await Table.AsQueryable().FirstOrDefaultAsync(x => x.OrderId == order.OrderId);
            if (item != null)
                return new GetOrderResult()
                {
                    OrderId = item.OrderId
                };

            return null;
        }
    }
}
