using System;
using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Models;

namespace Lab2.InventorySystem.Items.Armor
{
    public class Armor : BaseItem, IArmor
    {
        public override ItemType Type => ItemType.Armor;
        public int Defense { get; private set; }
        public ArmorSlot Slot { get; private set; }
        public bool IsEquipped { get; set; }
        
        private Armor(string id, string name, string description, ItemRarity rarity,
            int weight, int defense, ArmorSlot slot)
            : base(id, name, description, rarity, weight)
        {
            Defense = defense;
            Slot = slot;
        }

        public class Builder
        {
            private string _id;
            private string _name;
            private string _description;
            private ItemRarity _rarity = ItemRarity.Common;
            private int _weight = 1;
            private int _defense;
            private ArmorSlot _slot;

            public Builder(string id, string name, ArmorSlot slot)
            {
                _id = id;
                _name = name;
                _slot = slot;
                _description = $"{name} - {slot} броня";
            }

            public Builder WithDescription(string description)
            {
                _description = description;
                return this;
            }

            public Builder WithRarity(ItemRarity rarity)
            {
                _rarity = rarity;
                return this;
            }

            public Builder WithWeight(int weight)
            {
                _weight = weight;
                return this;
            }

            public Builder WithDefense(int defense)
            {
                _defense = defense;
                return this;
            }

            public Armor Build()
            {
                if (_defense < 0)
                    throw new InvalidOperationException("Защита не может быть меньше 0");

                return new Armor(_id, _name, _description, _rarity, _weight, _defense, _slot);
            }
        }
    }
}