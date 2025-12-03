using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Factory;

public class ExpressOrderFactory : IOrderFactory
{
    public Order CreateOrder(int id, List<OrderItem> items, Customer customer, DeliveryType deliveryType)
    {
        return new ExpressOrder(id, items, customer, deliveryType);
    }
}