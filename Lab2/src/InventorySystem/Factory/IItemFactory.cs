using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Interfaces;
using Lab2.InventorySystem.Items.Armor;
using Lab2.InventorySystem.Items.Potion;
using Lab2.InventorySystem.Items.Weapon;

namespace Lab2.InventorySystem.Factory
{
    public interface IItemFactory
    {
        IWeapon CreateWeapon(string name, ItemRarity rarity);
        IArmor CreateArmor(string name, ArmorSlot slot, ItemRarity rarity);
        IPotion CreatePotion(string name, ItemRarity rarity);
        IItem CreateQuestItem(string name, string description);
    }
}