using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Interfaces;

namespace Lab2.InventorySystem.Core.Models
{
    public abstract class BaseItem : IItem
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public abstract ItemType Type { get; }
        public ItemRarity Rarity { get; protected set; }
        public int Weight { get; protected set; }
        public virtual bool IsStackable => false;
        public virtual int MaxStackSize => 1;
        public int Quantity { get; set; } = 1;

        protected BaseItem(string id, string name, string description, ItemRarity rarity, int weight)
        {
            Id = id;
            Name = name;
            Description = description;
            Rarity = rarity;
            Weight = weight;
        }
    }
}