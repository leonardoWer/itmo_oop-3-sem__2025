using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.State;

public class ConfirmedOrderState : IOrderState
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} готовится");
        order.UpdateStatus(OrderStatus.Preparing);
        order.State = new PreparingOrderState();
    }
        
    public void CancelOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} отменен");
        order.UpdateStatus(OrderStatus.Cancelled);
        order.State = new CancelledOrderState();
    }
        
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} еще не готов для доставки");
    }
        
    public void CompleteOrder(Order order)
    {
        Console.WriteLine($"Невозможно завершить подтвержденный заказ #{order.Id}");
    }
        
    public string GetStatusName() => "Подтвержден";
}