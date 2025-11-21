using Lab2.InventorySystem.Core.Enums;

namespace Lab2.InventorySystem.Core.Interfaces
{
    public interface IItem
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        ItemType Type { get; }
        ItemRarity Rarity { get; }
        int Weight { get; }
        bool IsStackable { get; }
        int MaxStackSize { get; }
        int Quantity { get; set; }
    }
}