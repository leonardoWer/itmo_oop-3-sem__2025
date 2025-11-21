using System;
using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Models;
using Lab2.InventorySystem.Factory;
using Lab2.InventorySystem.Items.Armor;
using Lab2.InventorySystem.Items.Potion;
using Lab2.InventorySystem.Items.Quest;
using Lab2.InventorySystem.Items.Weapon;

namespace Lab2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Создаём инвентарь
            var inventory = new Inventory(64);
            var factory = new ItemFactory();
            var inventoryManager = new InventoryManager(inventory);
            
            // Добавляем предметы по паттерну Builder
            var sword = new Weapon.Builder("sword_001", "Стальной меч")
                .WithDescription("Острый стальной меч рыцаря")
                .WithRarity(ItemRarity.Uncommon)
                .WithDamage(25)
                .WithAttackSpeed(1.2f)
                .WithWeight(10)
                .Build();

            var chestplate = new Armor.Builder("armor_001", "Кожаный доспех", ArmorSlot.Chest)
                .WithDescription("Прочный кожаный нагрудник")
                .WithRarity(ItemRarity.Common)
                .WithDefense(15)
                .WithWeight(5)
                .Build();

            var healthPotion = new Potion.Builder("potion_001", "Зелье здоровья")
                .WithDescription("Восстанавливает здоровье")
                .WithHealingPower(50)
                .WithWeight(1)
                .Build();
            healthPotion.Quantity = 3;

            var questItem = new QuestItem.Builder("quest_001", "Древний артефакт")
                .WithDescription("Таинственный артефакт для квеста")
                .Build();
            
            // Добавляем предметы в инвентарь
            inventory.AddItem(sword);
            inventory.AddItem(chestplate);
            inventory.AddItem(healthPotion);
            inventory.AddItem(questItem);

            inventoryManager.DisplayInventory();
            
            // Добавляем предметы через Abstract Factory pattern
            var factorySword = factory.CreateWeapon("Волшебный посох", ItemRarity.Rare);
            var factoryArmor = factory.CreateArmor("Магические поножи", ArmorSlot.Legs, ItemRarity.Uncommon);
            var factoryPotion = factory.CreatePotion("Сильное зелье", ItemRarity.Rare);
            inventory.AddItem(factorySword);
            inventory.AddItem(factoryArmor);
            inventory.AddItem(factoryPotion);

            inventoryManager.DisplayEquippedItems();
            
            // Использование предметов через Strategy pattern
            // Использование оружия (экипировка/снятие)
            Console.WriteLine(inventoryManager.UseItem(sword.Id));
            Console.WriteLine(inventoryManager.UseItem(sword.Id)); // Снимаем

            // Использование брони (экипировка)
            Console.WriteLine(inventoryManager.UseItem(chestplate.Id));

            // Использование зелья
            Console.WriteLine(inventoryManager.UseItem(healthPotion.Id));
            Console.WriteLine($"Осталось зелий: {healthPotion.Quantity}");

            // Использование квестового предмета
            Console.WriteLine(inventoryManager.UseItem(questItem.Id));
            Console.WriteLine();
            
            // Демонстрация State pattern - улучшение предметов

            // Создаем улучшаемый меч
            var upgradableSword = new UpgradableWeapon(
                new Weapon.Builder("upgrade_sword", "Улучшаемый меч")
                    .WithDamage(30)
                    .WithRarity(ItemRarity.Rare)
                    .Build()
            );

            inventoryManager.RegisterUpgradableItem(upgradableSword);
            inventory.AddItem(upgradableSword);

            Console.WriteLine($"До улучшения: {upgradableSword.Name} - урон {upgradableSword.Damage}");
            Console.WriteLine(inventoryManager.UpgradeItem(upgradableSword.Id));
            Console.WriteLine($"После улучшения: {upgradableSword.Name} - урон {upgradableSword.Damage}");
            Console.WriteLine(upgradableSword.UpgradeState.GetStateInfo());
            
            // Переполнение инвентаря
            var heavyItem = new Weapon.Builder("heavy", "Очень тяжелый предмет")
                .WithDamage(1)
                .WithWeight(1000)
                .Build();
            var canAdd = inventory.AddItem(heavyItem);
            Console.WriteLine($"Можно ли добавить тяжелый предмет: {canAdd}");
            Console.WriteLine();
        }
    }
}