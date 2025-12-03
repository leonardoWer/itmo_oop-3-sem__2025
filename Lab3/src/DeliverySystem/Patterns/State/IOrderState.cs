using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.State;

/*
 * Использую
 * - чтобы можно было контроллировать логику в зависимости от статуса заказа
 * - Чтобы не делать много ифоф
 */
public interface IOrderState
{
    void ProcessOrder(Order order);
    void DeliverOrder(Order order);
    void CompleteOrder(Order order);
    void CancelOrder(Order order);
    string GetStatusName();
}