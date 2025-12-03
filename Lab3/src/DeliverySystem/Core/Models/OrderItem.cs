namespace Lab3.DeliverySystem.Core.Models;

public class OrderItem(MenuItem menuItem, int quantity, string specialInstructions = "")
{
    public MenuItem MenuItem { get; set; } = menuItem;
    public int Quantity { get; set; } = quantity;
    public string SpecialInstructions { get; set; } = specialInstructions;
    
    public decimal GetTotalPrice()
    {
        return MenuItem.Price * Quantity;
    }
}