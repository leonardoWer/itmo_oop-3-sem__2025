using System.Collections.Generic;
using Lab2.InventorySystem.Core.Enums;

namespace Lab2.InventorySystem.Core.Interfaces;

public interface IInventory
{
    int Capacity { get; }
    int TotalWeight { get; }
    ICollection<IItem> Items { get; }
    bool AddItem(IItem item);
    bool RemoveItem(string itemId);
    IItem GetItem(string itemId);
    IEnumerable<IItem> GetItemsByType(ItemType type);
    void Clear();
}