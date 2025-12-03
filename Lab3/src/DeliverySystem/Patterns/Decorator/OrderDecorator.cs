namespace Lab3.DeliverySystem.Patterns.Decorator;

public abstract class OrderDecorator(IOrderComponent component) : IOrderComponent
{
    protected readonly IOrderComponent _component = component;

    public virtual string GetDescription() => _component.GetDescription();
    
    public virtual decimal GetCost() => _component.GetCost();

    public virtual List<string> GetFeatures() =>  _component.GetFeatures();
}