using Lab3.DeliverySystem.Common;
using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Strategy;

public class StandardPricingStrategy : IPricingStrategy
{
    public decimal CalculatePrice(Order order)
    {
        decimal itemsTotal = order.Items.Sum(item => item.GetTotalPrice());
        decimal tax = itemsTotal * TaxConstants.STANDARD_TAX;
        decimal deliveryFee = GetDeliveryCost(order);
            
        return itemsTotal + tax + deliveryFee;
    }
    
    public decimal GetDeliveryCost(Order order)
    {
        decimal itemsTotal = order.Items.Sum(item => item.GetTotalPrice());
            
        if (itemsTotal >= DeliveryConstants.FREE_DELIVERY_THRESHOLD)
            return 0;
                
        return order.DeliveryType switch
        {
            DeliveryType.Standard => DeliveryConstants.STANDARD_DELIVERY_COST,
            DeliveryType.Express => DeliveryConstants.EXPRESS_DELIVERY_COST,
            DeliveryType.Scheduled => DeliveryConstants.SCHEDULED_DELIVERY_COST,
            _ => DeliveryConstants.STANDARD_DELIVERY_COST
        };
    }
        
    public string GetStrategyName() => "Стандартный расчет";
}