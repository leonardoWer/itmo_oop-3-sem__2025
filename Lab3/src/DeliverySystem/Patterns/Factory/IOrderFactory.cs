using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Factory;

// Позволяет подклассам решать, какой вид заказа инстанцировать
public interface IOrderFactory
{
    Order CreateOrder(int id, List<OrderItem> items, Customer customer, DeliveryType deliveryType);
}