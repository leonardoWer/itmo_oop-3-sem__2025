using Lab3.DeliverySystem.Core.Models;

namespace Lab3.DeliverySystem.Patterns.Strategy;

public class PricingContext(IPricingStrategy strategy)
{
    private IPricingStrategy _strategy = strategy;

    public void SetStrategy(IPricingStrategy strategy)
    {
        _strategy = strategy;
    }
        
    public decimal CalculatePrice(Order order)
    {
        return _strategy.CalculatePrice(order);
    }
        
    public string GetStrategyInfo()
    {
        return _strategy.GetStrategyName();
    }
}