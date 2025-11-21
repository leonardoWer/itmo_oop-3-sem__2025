using System.Collections.Generic;
using System.Linq;
using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Interfaces;

namespace Lab2.InventorySystem.Core.Models;

public class Inventory : IInventory
{
    private readonly List<IItem> _items;
    private readonly int _capacity;
    
    public int Capacity => _capacity;
    public int TotalWeight => _items.Sum(item => item.Weight * item.Quantity);
    public ICollection<IItem> Items => _items.AsReadOnly();
    
    public Inventory(int capacity = 64)
    {
        _capacity = capacity;
        _items = new List<IItem>();
    }
    
    public bool AddItem(IItem item)
    {
        if (item == null)
            return false;

        // Проверка веса
        if (TotalWeight + item.Weight * item.Quantity > _capacity)
            return false;

        // Для складываемых предметов
        if (item.IsStackable)
        {
            var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                if (existingItem.Quantity + item.Quantity <= existingItem.MaxStackSize)
                {
                    existingItem.Quantity += item.Quantity;
                    return true;
                }
            }
        }

        // Добавление нового предмета
        if (_items.Count < _capacity)
        {
            _items.Add(item);
            return true;
        }

        return false;
    }
    
    public bool RemoveItem(string itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            return _items.Remove(item);
        }
        return false;
    }
    
    public IItem GetItem(string itemId) => _items.FirstOrDefault(i => i.Id == itemId);
    
    public IEnumerable<IItem> GetItemsByType(ItemType type) => 
        _items.Where(item => item.Type == type);

    public void Clear() => _items.Clear();
}