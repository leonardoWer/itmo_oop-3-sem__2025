using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.State;

public class OnDeliveryOrderState : IOrderState
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} уже в доставке");
    }
        
    public void CancelOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} в процессе доставки. Отмена возможна только по уважительной причине.");
    }
        
    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} доставляется клиенту");
    }
        
    public void CompleteOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} доставлен!");
        order.UpdateStatus(OrderStatus.Delivered);
        order.State = new DeliveredOrderState();
    }
        
    public string GetStatusName() => "В доставке";
}