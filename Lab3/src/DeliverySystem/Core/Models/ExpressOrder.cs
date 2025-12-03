using Lab3.DeliverySystem.Common;
using Lab3.DeliverySystem.Core.Enums;

namespace Lab3.DeliverySystem.Core.Models;
public class ExpressOrder : Order
{
    public ExpressOrder(int id, List<OrderItem> items, Customer customer, DeliveryType deliveryType) 
        : base(id, items, customer, deliveryType)
    {}

    public override decimal CalculateTotal()
    {
        decimal itemsTotal = Items.Sum(item => item.GetTotalPrice());
        
        // Применяем коэффициент за срочность
        decimal expressItemsTotal = itemsTotal * OrderConstants.EXPRESS_SURCHARGE;
        
        // Проверяем ночное время для наценки
        if (IsNightTime())
        {
            expressItemsTotal *= OrderConstants.NIGHT_SURCHARGE;
        }
        
        decimal tax = CalculateTax(expressItemsTotal);
        decimal deliveryCost = DeliveryConstants.EXPRESS_DELIVERY_COST;
        
        return expressItemsTotal + tax + deliveryCost;
    }
    
    private decimal CalculateTax(decimal amount)
    {
        decimal totalTax = 0;
        
        // Рассчитываем налог для каждой категории отдельно
        foreach (var item in Items)
        {
            decimal itemTaxRate = GetTaxRateForCategory(item.MenuItem.Category);
            totalTax += item.GetTotalPrice() * itemTaxRate;
        }
        
        return totalTax * OrderConstants.EXPRESS_SURCHARGE;
    }
    
    private decimal GetTaxRateForCategory(string category)
    {
        if (TaxConstants.CATEGORY_TAX_RATES.TryGetValue(category, out decimal rate))
        {
            return rate;
        }
        return TaxConstants.STANDARD_TAX;
    }
    
    private bool IsNightTime()
    {
        int currentHour = DateTime.Now.Hour;
        return currentHour >= OrderConstants.NIGHT_START_HOUR || 
               currentHour < OrderConstants.NIGHT_END_HOUR;
    }
    
    public override void DisplayOrderInfo()
    {
        base.DisplayOrderInfo();
        Console.WriteLine("Тип заказа: Экспресс");
        Console.WriteLine($"Время доставки: {DeliveryConstants.EXPRESS_DELIVERY_TIME} минут");
    }
}