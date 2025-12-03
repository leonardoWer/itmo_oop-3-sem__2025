using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.State;

public class NewOrderState : IOrderState
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} подтвержден и передан на кухню");
        order.UpdateStatus(OrderStatus.Confirmed);
        order.State = new ConfirmedOrderState();
    }

    public void DeliverOrder(Order order)
    {
        Console.WriteLine($"Невозможно доставить новый заказ #{order.Id}. Сначала подтвердите его");
    }

    public void CompleteOrder(Order order)
    {
        Console.WriteLine($"Невозможно завершить новый заказ #{order.Id}");
    }

    public void CancelOrder(Order order)
    {
        Console.WriteLine($"Заказ #{order.Id} отменен");
        order.UpdateStatus(OrderStatus.Cancelled);
        order.State = new CancelledOrderState();
    }

    public string GetStatusName() => "Новый";
}