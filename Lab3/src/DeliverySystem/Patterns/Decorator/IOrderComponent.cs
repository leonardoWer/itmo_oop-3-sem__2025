namespace Lab3.DeliverySystem.Patterns.Decorator;

public interface IOrderComponent
{
    string GetDescription();
    decimal GetCost();
    List<string> GetFeatures();
}