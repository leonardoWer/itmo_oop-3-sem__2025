namespace Lab3.DeliverySystem.Core.Models;

public class Customer(int id, string name, string address, string phone)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Address { get; set; } = address;
    public string Phone { get; set; } = phone;
    public string Email { get; set; } = "";
    public bool IsLoyaltyMember { get; set; } = false;
    
    public void SetEmail(string email) => Email = email;
    
    public void ActivateLoyalty()
    {
        IsLoyaltyMember = true;
    }
}