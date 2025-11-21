using Lab2.InventorySystem.Core.Interfaces;

namespace Lab2.InventorySystem.Items.Armor
{
    public interface IArmor : IItem
    {
        int Defense { get; }
        ArmorSlot Slot { get; }
        bool IsEquipped { get; set; }
    }
}