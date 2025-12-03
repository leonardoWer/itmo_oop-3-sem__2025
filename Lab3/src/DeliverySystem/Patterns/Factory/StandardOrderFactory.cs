using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Factory;

public class StandardOrderFactory : IOrderFactory
{
    public Order CreateOrder(int id, List<OrderItem> items, Customer customer, DeliveryType deliveryType)
    {
        return new StandardOrder(id, items, customer, deliveryType);
    }
}