using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Services.Interfaces;

namespace Lab3.DeliverySystem.Patterns.Command;

public class UpdateOrderStatusCommand : ICommand
{
    private readonly IOrderService _orderService;
    private readonly int _orderId;
    private readonly OrderStatus _newStatus;
    private OrderStatus _previousStatus;
    
    public UpdateOrderStatusCommand(IOrderService orderService, int orderId, OrderStatus newStatus)
    {
        _orderService = orderService;
        _orderId = orderId;
        _newStatus = newStatus;
    }
    
    public void Execute()
    {
        var order = _orderService.GetOrder(_orderId);
        if (order != null)
        {
            _previousStatus = order.Status;
            _orderService.UpdateOrderStatus(_orderId, _newStatus);
            Console.WriteLine($"Выполнено: {GetDescription()}");
        }
    }

    public void Undo()
    {
        if (_previousStatus != default)
        {
            _orderService.UpdateOrderStatus(_orderId, _previousStatus);
            Console.WriteLine($"Отменено изменение статуса заказа #{_orderId}");
        }
    }
    public string GetDescription() => $"Изменить статус заказа #{_orderId} на {_newStatus}";
}