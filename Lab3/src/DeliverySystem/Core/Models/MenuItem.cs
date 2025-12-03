namespace Lab3.DeliverySystem.Core.Models;

public class MenuItem(int id, string name, string description, decimal price, string category)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
    public string Category { get; set; } = category;
    public int PreparationTime { get; set; }
    
    public override string ToString()
    {
        return $"{Name} - {Price} руб. ({Category})";
    }
}