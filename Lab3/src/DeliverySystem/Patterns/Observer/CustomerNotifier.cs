using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Observer;

public class CustomerNotifier : IOrderObserver
{
    public void Update(Order order, string message)
    {
        // Здесь могла бы быть реальная отправка SMS или email
        Console.WriteLine($"[Уведомление клиенту {order.Customer.Name}]");
        Console.WriteLine($"Заказ #{order.Id}: {message}");
        Console.WriteLine($"Телефон: {order.Customer.Phone}");
        Console.WriteLine($"Email: {order.Customer.Email}");
        Console.WriteLine();
    }
    
    public string GetObserverName() => "Уведомления для клиента на телефон или email";
}