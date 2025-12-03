using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Observer;

// Объект, за котороым я слежу
public class OrderSubject(Order order)
{
    private readonly List<IOrderObserver> _observers = new();
    private Order _order = order;

    public void Attach(IOrderObserver observer)
    {
        _observers.Add(observer);
        Console.WriteLine($"Наблюдатель {observer.GetObserverName()} подключен к заказу #{_order.Id}");
    }
        
    public void Detach(IOrderObserver observer)
    {
        _observers.Remove(observer);
        Console.WriteLine($"Наблюдатель {observer.GetObserverName()} отключен от заказа #{_order.Id}");
    }
        
    public void Notify(string message)
    {
        Console.WriteLine($"=== Отправка уведомлений для заказа #{_order.Id} ===");
        foreach (var observer in _observers)
        {
            observer.Update(_order, message);
        }
    }
        
    public void UpdateOrder(Order order)
    {
        _order = order;
        Notify("Заказ обновлен");
    }
}