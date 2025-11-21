using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Interfaces;

namespace Lab2.InventorySystem.Strategy
{
    public class QuestItemUsageStrategy : IItemUsageStrategy
    {
        public bool CanUse(IItem item) => item.Type == ItemType.Quest;

        public string Use(IItem item)
        {
            return $"Квестовый предмет {item.Name} можно использовать только через NPC";
        }
    }
}