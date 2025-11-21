using Lab2.InventorySystem.Core.Interfaces;

namespace Lab2.InventorySystem.Items.Potion
{
    public interface IPotion : IItem
    {
        int Power { get; }
        void Consume();
    }
}