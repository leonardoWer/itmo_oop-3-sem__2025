using Lab3.DeliverySystem.Common;
using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Strategy;

public class LoyaltyPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(Order order)
    {
        decimal itemsTotal = order.Items.Sum(item => item.GetTotalPrice());
        decimal tax = itemsTotal * TaxConstants.STANDARD_TAX;
        decimal deliveryFee = GetDeliveryCost(order);
        decimal total = itemsTotal + tax + deliveryFee;
            
        // Скидка для лояльных клиентов
        if (order.Customer.IsLoyaltyMember)
        {
            total *= (1 - LoyaltyConstants.LOYALTY_DISCOUNT);
        }
            
        return total;
    }

    public decimal GetDeliveryCost(Order order)
    {
        decimal itemsTotal = order.Items.Sum(item => item.GetTotalPrice());
            
        // Лояльные клиенты получают бесплатную доставку от меньшей суммы
        if (order.Customer.IsLoyaltyMember && itemsTotal >= 500)
            return 0;
                
        return order.DeliveryType switch
        {
            DeliveryType.Standard => DeliveryConstants.STANDARD_DELIVERY_COST,
            DeliveryType.Express => DeliveryConstants.EXPRESS_DELIVERY_COST,
            DeliveryType.Scheduled => DeliveryConstants.SCHEDULED_DELIVERY_COST,
            _ => DeliveryConstants.STANDARD_DELIVERY_COST
        };
    }

    public string GetStrategyName()
    {
        throw new NotImplementedException();
    }
}