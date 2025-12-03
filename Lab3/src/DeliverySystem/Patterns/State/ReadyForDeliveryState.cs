using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.State;

public class ReadyForDeliveryState : IOrderState
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} передан курьеру");
        order.UpdateStatus(OrderStatus.OnDelivery);
        order.State = new OnDeliveryOrderState();
    }
        
    public void CancelOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} отменен. Частичный возврат средств.");
        order.UpdateStatus(OrderStatus.Cancelled);
        order.State = new CancelledOrderState();
    }
        
    public void DeliverOrder(Order order)
    {
        ProcessOrder(order);
    }
        
    public void CompleteOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} еще не доставлен");
    }
        
    public string GetStatusName() => "Готов к доставке";
}