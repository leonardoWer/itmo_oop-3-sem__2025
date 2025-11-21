using Lab2.InventorySystem.Core.Interfaces;

namespace Lab2.InventorySystem.Items.Weapon
{
    public interface IWeapon : IItem
    {
        int Damage { get; }
        float AttackSpeed { get; }
        bool IsEquipped { get; set; }
    }
}