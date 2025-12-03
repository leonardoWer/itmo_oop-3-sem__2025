using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.State;

public class CancelledOrderState : IOrderState
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} отменен. Невозможно обработать.");
    }
        
    public void CancelOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} уже отменен");
    }
        
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} отменен. Доставка невозможна.");
    }
        
    public void CompleteOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} отменен. Завершение невозможно.");
    }
        
    public string GetStatusName() => "Отменен";
}