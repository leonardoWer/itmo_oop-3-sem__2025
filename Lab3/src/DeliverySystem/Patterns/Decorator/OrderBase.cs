using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Decorator;

public class OrderBase(Order order) : IOrderComponent
{
    protected readonly Order _order = order;
    
    public virtual string GetDescription() => $"Заказ #{_order.Id} для {_order.Customer.Name}";

    public virtual decimal GetCost()
    {
        return _order.CalculateTotal();
    }

    public virtual List<string> GetFeatures()
    {
        return new List<string>()
        {
            $"Тип доставки {_order.DeliveryType}",
            $"Статус заказа {_order.State.GetStatusName()}"
        };
    }
}