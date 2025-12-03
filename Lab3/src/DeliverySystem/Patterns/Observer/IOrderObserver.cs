using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Observer;

/*
 * Использую
 * -> Для уведомлений
 * - Изменение статуса заказа требует обновления других классов
 */
public interface IOrderObserver
{
    void Update(Order order, string message);
    string GetObserverName();
}