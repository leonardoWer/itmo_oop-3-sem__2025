using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Patterns.State;

namespace Lab3.DeliverySystem.Core.Models;

public abstract class Order
{
    public int Id { get; }
    public List<OrderItem> Items { get; }
    public OrderStatus Status { get; set; }
    public Customer Customer { get; }
    public DateTime OrderDate { get; }
    public DeliveryType DeliveryType { get; }
    public string DeliveryAddress { get; set; }
    public string SpecialInstructions { get; set; }
    
    // Логика отслеживания состояния заказаа с помощью State
    public IOrderState State { get; set; }

    protected Order(int id, List<OrderItem> items, Customer customer, DeliveryType deliveryType)
    {
        Id = id;
        Items = items;
        Customer = customer;
        Status = OrderStatus.New;
        OrderDate = DateTime.Now;
        DeliveryType = deliveryType;
        DeliveryAddress = customer.Address;
        SpecialInstructions = "";
        State = new NewOrderState();
    }

    public void AddItem(OrderItem item) => Items.Add(item);
    public void RemoveItem(OrderItem item) => Items.Remove(item);
    public void UpdateStatus(OrderStatus newStatus)
    {
        Status = newStatus;
    }

    protected abstract decimal CalculateTotal();
    
    // Разная логика в зависимости от состояния
    public void Process() => State.ProcessOrder(this);
    public void Cancel() => State.CancelOrder(this);
    public void Deliver() => State.DeliverOrder(this);
    public void Complete() => State.CompleteOrder(this);
    
    public virtual void DisplayOrderInfo()
    {
        Console.WriteLine($"Заказ #{Id}");
        Console.WriteLine($"Клиент: {Customer.Name}");
        Console.WriteLine($"Адрес доставки: {DeliveryAddress}");
        Console.WriteLine($"Тип доставки: {DeliveryType}");
        Console.WriteLine($"Статус: {Status}");
        Console.WriteLine($"Дата заказа: {OrderDate}");
        Console.WriteLine("Состав заказа:");
            
        foreach (var item in Items)
        {
            Console.WriteLine($"  - {item.MenuItem.Name} x{item.Quantity}: {item.GetTotalPrice()} руб.");
            if (!string.IsNullOrEmpty(item.SpecialInstructions))
            {
                Console.WriteLine($"Примечание: {item.SpecialInstructions}");
            }
        }
            
        Console.WriteLine($"Итого: {CalculateTotal()} руб.");
    }
}