using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Observer;

public class KitchenNotifier : IOrderObserver
{
    public void Update(Order order, string message)
    {
        Console.WriteLine($"[Уведомление кухне]");
        Console.WriteLine($"Заказ #{order.Id}: {message}");
        Console.WriteLine($"Состав заказа:");
        foreach (var item in order.Items)
        {
            Console.WriteLine($" - {item.MenuItem.Name} x{item.Quantity}");
            if (!string.IsNullOrEmpty(item.SpecialInstructions))
            {
                Console.WriteLine($"Примечание: {item.SpecialInstructions}");
            }
        }
        Console.WriteLine($"  Общее время приготовления: {CalculatePreparationTime(order)} мин.");
        Console.WriteLine();
    }
        
    private int CalculatePreparationTime(Order order)
    {
        return order.Items.Sum(item => item.MenuItem.PreparationTime) + 5; // +5 мин на упаковку
    }
        
    public string GetObserverName() => "Уведомления для кухни";
}