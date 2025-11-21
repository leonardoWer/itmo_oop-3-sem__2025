using System;
using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Models;

namespace Lab2.InventorySystem.Items.Potion
{
    public class Potion : BaseItem, IPotion
    {
        public override ItemType Type => ItemType.Potion;
        public override bool IsStackable => true;
        public override int MaxStackSize => 10;
        public int Power { get; private set; }
        
        private Potion(string id, string name, string description, ItemRarity rarity,
            int weight, int power)
            : base(id, name, description, rarity, weight)
        {
            Power = power;
        }

        public void Consume()
        {
            if (Quantity > 0)
            {
                Quantity--;
                // Логика применения зелья в сервисе использования
            }
        }

        public class Builder
        {
            private string _id;
            private string _name;
            private string _description;
            private ItemRarity _rarity = ItemRarity.Common;
            private int _weight = 1;
            private int _power;

            public Builder(string id, string name)
            {
                _id = id;
                _name = name;
                _description = $"{name} - восстанавливает здоровье";
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

            public Builder WithHealingPower(int healingPower)
            {
                _power = healingPower;
                return this;
            }

            public Potion Build()
            {
                if (_power <= 0)
                    throw new InvalidOperationException("Healing power must be positive");

                return new Potion(_id, _name, _description, _rarity, _weight, _power);
            }
        }
    }
}