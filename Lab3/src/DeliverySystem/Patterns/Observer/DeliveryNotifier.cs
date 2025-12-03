using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Observer;

public class DeliveryNotifier : IOrderObserver
{
    public void Update(Order order, string message)
    {
        Console.WriteLine($"[Уведомление службе доставки]");
        Console.WriteLine($"Заказ #{order.Id}: {message}");
        Console.WriteLine($"Адрес: {order.DeliveryAddress}");
        Console.WriteLine($"Тип доставки: {order.DeliveryType}");
        Console.WriteLine($"Контакт клиента: {order.Customer.Phone}");
        Console.WriteLine();
    }
        
    public string GetObserverName() => "Уведомления для службы доставки";
}