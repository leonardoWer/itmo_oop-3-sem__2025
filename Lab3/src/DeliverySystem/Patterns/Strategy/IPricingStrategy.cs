using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Strategy;

/*
 * Использую
 * - чтобы можно было вычислять разную стоимость заказа в зависимости от разных условий
 * - Легко добавлять нотвые стратегии оплаты
 */
public interface IPricingStrategy
{
    decimal CalculatePrice(Order order);
    decimal GetDeliveryCost(Order order);
    string GetStrategyName();
}