using Lab2.InventorySystem.Core.Interfaces;
using Lab2.InventorySystem.Items.Armor;

namespace Lab2.InventorySystem.Strategy
{
    // Паттерн Strategy для использования предметов
    public class ArmorUsageStrategy : IItemUsageStrategy
    {
        public bool CanUse(IItem item) => item is IArmor;

        public string Use(IItem item)
        {
            var armor = item as IArmor;
            armor.IsEquipped = !armor.IsEquipped;
            return armor.IsEquipped ? $"Броня {armor.Name} экипирована" : $"Броня {armor.Name} снята";
        }
    }
}