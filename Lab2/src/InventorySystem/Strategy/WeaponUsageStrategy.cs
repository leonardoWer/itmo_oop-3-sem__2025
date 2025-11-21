using Lab2.InventorySystem.Core.Interfaces;
using Lab2.InventorySystem.Items.Weapon;

namespace Lab2.InventorySystem.Strategy
{
    // Паттерн Strategy для использования предметов
    public class WeaponUsageStrategy : IItemUsageStrategy
    {
        public bool CanUse(IItem item) => item is IWeapon;

        public string Use(IItem item)
        {
            var weapon = item as IWeapon;
            weapon.IsEquipped = !weapon.IsEquipped;
            return weapon.IsEquipped ? $"Оружие {weapon.Name} экипировано" : $"Оружие {weapon.Name} снято";
        }
    }
}