using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.State;

public class DeliveredOrderState : IOrderState
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} уже доставлен");
    }
        
    public void CancelOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} уже доставлен. Отмена невозможна.");
    }
        
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} уже доставлен");
    }
        
    public void CompleteOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} завершен успешно");
    }
        
    public string GetStatusName() => "Доставлен";
}