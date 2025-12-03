namespace Lab3.DeliverySystem.Patterns.Decorator;

public class UrgentDecorator(IOrderComponent component) : OrderDecorator(component)
{
    private const decimal URGENT_COST = 300;
        
    public override string GetDescription()
    {
        return _component.GetDescription() + " (срочный заказ)";
    }
        
    public override decimal GetCost()
    {
        return _component.GetCost() + URGENT_COST;
    }
        
    public override List<string> GetFeatures()
    {
        var features = _component.GetFeatures();
        features.Add("Срочный заказ");
        features.Add("Приоритетная обработка");
        features.Add("Гарантированное время доставки");
        return features;
    }
}