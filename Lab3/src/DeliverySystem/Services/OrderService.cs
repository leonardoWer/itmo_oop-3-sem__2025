using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;
using Lab3.DeliverySystem.Patterns.Factory;
using Lab3.DeliverySystem.Services.Interfaces;

namespace Lab3.DeliverySystem.Services;

public class OrderService : IOrderService
{
    private List<Order> _orders = new List<Order>();
    private int _nextOrderId = 1;
    
    // Фабрики для разных типов заказа
    private readonly Dictionary<string, IOrderFactory> _factories = new()
    {
        { "standard", new StandardOrderFactory() },
        { "express", new ExpressOrderFactory() }
    };

    public Order CreateOrder(Customer customer, List<OrderItem> items, DeliveryType deliveryType, 
        string orderType = "standard")
    {
        if (!_factories.ContainsKey(orderType.ToLower()))
        {
            throw new ArgumentException($"Неизвестный тип заказа: {orderType}");
        }
            
        var factory = _factories[orderType.ToLower()];
        var order = factory.CreateOrder(_nextOrderId++, items, customer, deliveryType);
            
        _orders.Add(order);
        return order;
    }

    public Order? GetOrder(int orderId)
    {
        return _orders.FirstOrDefault(o => o.Id == orderId);
    }

    public void UpdateOrderStatus(int orderId, OrderStatus status)
    {
        var order = GetOrder(orderId);
        if (order != null)
        {
            order.UpdateStatus(status);
        }
    }

    public List<Order> GetCustomerOrders(int customerId)
    {
        return _orders.Where(o => o.Customer.Id == customerId).ToList();
    }
}