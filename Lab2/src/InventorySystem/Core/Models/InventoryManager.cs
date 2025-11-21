using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab2.InventorySystem.Core.Interfaces;
using Lab2.InventorySystem.Items.Armor;
using Lab2.InventorySystem.Items.Potion;
using Lab2.InventorySystem.Items.Weapon;
using Lab2.InventorySystem.State;
using Lab2.InventorySystem.Strategy;

namespace Lab2.InventorySystem.Core.Models;

public class InventoryManager : IInventoryManager
{
    private readonly IInventory _inventory;
    private readonly IItemUsageStrategy[] _usageStrategies;
    private readonly Dictionary<string, IUpgradableItem> _upgradableItems;
    
    public IInventory Inventory => _inventory;
    
    public InventoryManager(IInventory inventory)
    {
        _inventory = inventory;
        _usageStrategies = new IItemUsageStrategy[]
        {
            new WeaponUsageStrategy(),
            new ArmorUsageStrategy(),
            new PotionUsageStrategy(),
            new QuestItemUsageStrategy()
        };
        
        _upgradableItems = new Dictionary<string, IUpgradableItem>();
    }
    
    public string UseItem(string itemId)
    {
        var item = _inventory.GetItem(itemId);
        if (item == null)
            return "Предмет не найден";

        var strategy = _usageStrategies.FirstOrDefault(s => s.CanUse(item));
        if (strategy == null)
            return $"Неизвестный тип предмета: {item.Type}";

        var result = strategy.Use(item);

        // Удаляем предмет если количество стало 0 (для зелий)
        if (item.Quantity <= 0)
        {
            _inventory.RemoveItem(itemId);
        }

        return result;
    }
    
    public string UpgradeItem(string itemId)
    {
        if (!_upgradableItems.ContainsKey(itemId))
            return "Этот предмет нельзя улучшить";

        var upgradableItem = _upgradableItems[itemId];
        return upgradableItem.UpgradeState.Upgrade(upgradableItem);
    }
    
    public string DisplayInventory()
    {
        if (!_inventory.Items.Any())
            return "Инвентарь пуст";

        var sb = new StringBuilder();
        sb.AppendLine("=== ИНВЕНТАРЬ ===");
        sb.AppendLine($"Вместимость: {_inventory.Items.Count}/{_inventory.Capacity}");
        sb.AppendLine($"Общий вес: {_inventory.TotalWeight}");

        // Группируем по типу
        foreach (var group in _inventory.Items.GroupBy(i => i.Type))
        {
            sb.AppendLine($"\n--- {group.Key} ---");
            foreach (var item in group)
            {
                sb.AppendLine($"{item.Name} (x{item.Quantity}), весом по {item.Weight}");
                if (item is IWeapon weapon)
                {
                    sb.AppendLine($"Урон: {weapon.Damage}, Скорость: {weapon.AttackSpeed}");
                    sb.AppendLine($"Экипировано: {(weapon.IsEquipped ? "Да" : "Нет")}");
                }
                else if (item is IArmor armor)
                {
                    sb.AppendLine($"    Защита: {armor.Defense}, Слот: {armor.Slot}");
                    sb.AppendLine($"    Экипировано: {(armor.IsEquipped ? "Да" : "Нет")}");
                }
                else if (item is IPotion potion)
                {
                    sb.AppendLine($"Зелье силой: {potion.Power}");
                }

                if (_upgradableItems.ContainsKey(item.Id))
                {
                    sb.AppendLine($"{_upgradableItems[item.Id].UpgradeState.GetStateInfo()}");
                }
            }
        }

        return sb.ToString();
    }
    
    public string DisplayEquippedItems()
    {
        var equippedWeapons = _inventory.Items.OfType<IWeapon>().Where(w => w.IsEquipped);
        var equippedArmor = _inventory.Items.OfType<IArmor>().Where(a => a.IsEquipped);

        if (!equippedWeapons.Any() && !equippedArmor.Any())
            return "Нет экипированных предметов";

        var sb = new StringBuilder();
        sb.AppendLine("=== ЭКИПИРОВКА ===");

        if (equippedWeapons.Any())
        {
            sb.AppendLine("Оружие:");
            foreach (var weapon in equippedWeapons)
            {
                sb.AppendLine($"{weapon.Name} - {weapon.Damage} урона");
            }
        }

        if (equippedArmor.Any())
        {
            sb.AppendLine("Броня:");
            foreach (var armor in equippedArmor)
            {
                sb.AppendLine($"{armor.Name} ({armor.Slot}) - {armor.Defense} защиты");
            }
        }

        return sb.ToString();
    }
    
    public bool CombineItems(string sourceItemId, string targetItemId)
    {
        // Базовая реализация комбинирования
        var sourceItem = _inventory.GetItem(sourceItemId);
        var targetItem = _inventory.GetItem(targetItemId);

        if (sourceItem == null || targetItem == null)
            return false;

        // Простое комбинирование - увеличение количества
        if (sourceItem.Id == targetItem.Id && sourceItem.IsStackable)
        {
            targetItem.Quantity += sourceItem.Quantity;
            _inventory.RemoveItem(sourceItemId);
            return true;
        }

        return false;
    }
    
    public void RegisterUpgradableItem(IUpgradableItem upgradableItem)
    {
        if (upgradableItem is IItem item)
        {
            _upgradableItems[item.Id] = upgradableItem;
        }
    }
}