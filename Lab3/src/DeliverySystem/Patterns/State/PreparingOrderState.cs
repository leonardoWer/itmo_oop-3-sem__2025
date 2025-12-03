using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.State;

public class PreparingOrderState : IOrderState
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} готов к доставке");
        order.UpdateStatus(OrderStatus.ReadyForDelivery);
        order.State = new ReadyForDeliveryState();
    }
        
    public void CancelOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} отменен. Возможен возврат средств.");
        order.UpdateStatus(OrderStatus.Cancelled);
        order.State = new CancelledOrderState();
    }
        
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} еще готовится");
    }
        
    public void CompleteOrder(Order order)
    {
        Console.WriteLine($"Невозможно завершить заказ #{order.Id} во время приготовления");
    }
        
    public string GetStatusName() => "Готовится";
}