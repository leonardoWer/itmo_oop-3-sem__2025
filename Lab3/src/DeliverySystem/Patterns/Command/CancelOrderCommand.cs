using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Services.Interfaces;

namespace Lab3.DeliverySystem.Patterns.Command;

public class CancelOrderCommand : ICommand
{
    private readonly IOrderService _orderService;
    private readonly int _orderId;
    private OrderStatus _previousStatus;
        
    public CancelOrderCommand(IOrderService orderService, int orderId)
    {
        _orderService = orderService;
        _orderId = orderId;
    }
        
    public void Execute()
    {
        var order = _orderService.GetOrder(_orderId);
        if (order != null)
        {
            _previousStatus = order.Status;
            _orderService.UpdateOrderStatus(_orderId, OrderStatus.Cancelled);
            Console.WriteLine($"Выполнено: {GetDescription()}");
        }
    }
        
    public void Undo()
    {
        if (_previousStatus != default)
        {
            _orderService.UpdateOrderStatus(_orderId, _previousStatus);
            Console.WriteLine($"Отменена отмена заказа #{_orderId}");
        }
    }
        
    public string GetDescription() => $"Отменить заказ #{_orderId}";
}