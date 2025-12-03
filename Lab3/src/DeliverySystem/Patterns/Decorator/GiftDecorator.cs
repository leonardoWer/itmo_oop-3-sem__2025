namespace Lab3.DeliverySystem.Patterns.Decorator;

public class GiftDecorator(IOrderComponent component) : OrderDecorator(component)
{
    private const decimal GIFT_WRAP_COST = 150m;
        
    public override string GetDescription()
    {
        return _component.GetDescription() + " (подарочная упаковка)";
    }
        
    public override decimal GetCost()
    {
        return _component.GetCost() + GIFT_WRAP_COST;
    }
        
    public override List<string> GetFeatures()
    {
        var features = _component.GetFeatures();
        features.Add("Подарочная упаковка");
        return features;
    }
}