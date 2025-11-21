using Lab2.InventorySystem.Core.Interfaces;

namespace Lab2.InventorySystem.Strategy
{
    public interface IItemUsageStrategy
    {
        bool CanUse(IItem item);
        string Use(IItem item);
    }
}