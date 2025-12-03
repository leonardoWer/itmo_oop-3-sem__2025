using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;
using Lab3.DeliverySystem.Services.Interfaces;

namespace Lab3.DeliverySystem.Patterns.Command;

public class CreateOrderCommand : ICommand
{
    private readonly IOrderService _orderService;
    private readonly List<OrderItem> _items;
    private readonly Customer _customer;
    private readonly DeliveryType _deliveryType;
    private readonly string _orderType;
    private Order? _createdOrder;
    
    public CreateOrderCommand(IOrderService orderService, List<OrderItem> items,
        Customer customer, DeliveryType deliveryType, 
        string orderType = "standard")
    {
        _orderService = orderService;
        _customer = customer;
        _items = items;
        _deliveryType = deliveryType;
        _orderType = orderType;
    }
    
    public void Execute()
    {
        _createdOrder = _orderService.CreateOrder(_items, _customer, _deliveryType, _orderType);
        Console.WriteLine($"Выполнено: {GetDescription()}");
    }

    public void Undo()
    {
        if (_createdOrder == null) return;
        
        Console.WriteLine($"Отменено создание заказа #{_createdOrder.Id}");
        _createdOrder = null;
    }

    public string GetDescription() => $"Создать заказ для {_customer.Name} ({_orderType})";
}