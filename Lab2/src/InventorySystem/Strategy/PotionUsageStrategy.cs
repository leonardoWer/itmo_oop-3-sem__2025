using Lab2.InventorySystem.Core.Interfaces;
using Lab2.InventorySystem.Items.Potion;

namespace Lab2.InventorySystem.Strategy
{
    public class PotionUsageStrategy : IItemUsageStrategy
    {
        public bool CanUse(IItem item) => item is IPotion && item.Quantity > 0;

        public string Use(IItem item)
        {
            var potion = item as IPotion;
            potion.Consume();
            return $"Зелье {potion.Name} силы {potion.Power} использовано";
        }
    }
}