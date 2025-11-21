using System;
using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Interfaces;
using Lab2.InventorySystem.Items.Armor;
using Lab2.InventorySystem.Items.Potion;
using Lab2.InventorySystem.Items.Quest;
using Lab2.InventorySystem.Items.Weapon;

namespace Lab2.InventorySystem.Factory
{
    // Abstract Factory для создания предметов
    public class ItemFactory
    {
        private int _itemCounter = 0;

        private string GenerateId() => $"item_{DateTime.Now.Ticks}_{++_itemCounter}";

        public IWeapon CreateWeapon(string name, ItemRarity rarity)
        {
            var damage = rarity switch
            {
                ItemRarity.Common => 10,
                ItemRarity.Uncommon => 20,
                ItemRarity.Rare => 35,
                ItemRarity.Epic => 50,
                ItemRarity.Legendary => 75,
                ItemRarity.Mythic => 100,
                _ => 5
            };
            
            return new Weapon.Builder(GenerateId(), name)
                .WithRarity(rarity)
                .WithDamage(damage)
                .WithWeight(2)
                .Build();
        }
        
        public IArmor CreateArmor(string name, ArmorSlot slot, ItemRarity rarity)
        {
            var defense = rarity switch
            {
                ItemRarity.Common => 5,
                ItemRarity.Uncommon => 10,
                ItemRarity.Rare => 20,
                ItemRarity.Epic => 35,
                ItemRarity.Legendary => 50,
                _ => 5
            };

            return new Armor.Builder(GenerateId(), name, slot)
                .WithRarity(rarity)
                .WithDefense(defense)
                .WithWeight(10)
                .Build();
        }
        
        public IPotion CreatePotion(string name, ItemRarity rarity)
        {
            var healing = rarity switch
            {
                ItemRarity.Common => 25,
                ItemRarity.Uncommon => 50,
                ItemRarity.Rare => 100,
                ItemRarity.Epic => 200,
                ItemRarity.Legendary => 500,
                _ => 25
            };

            return new Potion.Builder(GenerateId(), name)
                .WithRarity(rarity)
                .WithHealingPower(healing)
                .WithWeight(1)
                .Build();
        }

        public IItem CreateQuestItem(string name, string description)
        {
            return new QuestItem.Builder(GenerateId(), name).Build();
        }
    }
}