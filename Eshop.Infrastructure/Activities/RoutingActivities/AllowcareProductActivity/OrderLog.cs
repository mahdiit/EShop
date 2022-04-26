using Eshop.Infrastructure.Event.Order;

namespace Eshop.Infrastructure.Activities.RoutingActivities.AllowcareProductActivity
{
    public class OrderLog
    {
        public OrderCreated Order { get; set; }
        public string Message { get; set; }
    }
}
