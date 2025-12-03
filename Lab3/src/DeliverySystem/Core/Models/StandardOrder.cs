using Lab3.DeliverySystem.Common;
using Lab3.DeliverySystem.Core.Enums;

namespace Lab3.DeliverySystem.Core.Models;

public class StandardOrder : Order
{
    public StandardOrder(int id, List<OrderItem> items, Customer customer, DeliveryType deliveryType) 
        : base(id, items, customer, deliveryType)
    {}
    
    public override decimal CalculateTotal()
    {
        decimal itemsTotal = Items.Sum(item => item.GetTotalPrice());
        decimal tax = itemsTotal * TaxConstants.STANDARD_TAX;
            
        return itemsTotal + tax + GetDeliveryCost(itemsTotal);
    }
    
    private decimal GetDeliveryCost(decimal itemsTotal)
    {
        // Бесплатная доставка при большом заказе
        if (itemsTotal >= DeliveryConstants.FREE_DELIVERY_THRESHOLD)
        {
            return 0;
        }
        
        return DeliveryType switch
        {
            DeliveryType.Standard => DeliveryConstants.STANDARD_DELIVERY_COST,
            DeliveryType.Express => DeliveryConstants.EXPRESS_DELIVERY_COST,
            DeliveryType.Scheduled => DeliveryConstants.SCHEDULED_DELIVERY_COST,
            _ => DeliveryConstants.STANDARD_DELIVERY_COST
        };
    }
        
    public override void DisplayOrderInfo()
    {
        base.DisplayOrderInfo();
        Console.WriteLine("Тип заказа: Стандартный");
        Console.WriteLine($"Примерный срок доставки: {DeliveryConstants.STANDARD_DELIVERY_TIME} мин.");
    }
}