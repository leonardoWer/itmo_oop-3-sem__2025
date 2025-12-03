using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Services.Interfaces;

public interface IOrderService
{
    Order CreateOrder(Customer customer, List<OrderItem> items, 
        DeliveryType deliveryType, string orderType = "standard");
    Order? GetOrder(int orderId);
    void UpdateOrderStatus(int orderId, OrderStatus status);
    List<Order> GetCustomerOrders(int customerId);
}