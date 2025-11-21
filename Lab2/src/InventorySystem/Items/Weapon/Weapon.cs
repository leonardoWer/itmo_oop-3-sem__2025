using System;
using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Models;

namespace Lab2.InventorySystem.Items.Weapon
{
    public class Weapon : BaseItem, IWeapon
    {
        public override ItemType Type => ItemType.Weapon;
        public int Damage { get; protected set; }
        public float AttackSpeed { get; private set; }
        public bool IsEquipped { get; set; }

        protected Weapon(string id, string name, string description, ItemRarity rarity, 
            int weight, int damage, float attackSpeed) 
            : base(id, name, description, rarity, weight)
        {
            Damage = damage;
            AttackSpeed = attackSpeed;
        }
        
        public class Builder
        {
            private string _id;
            private string _name;
            private string _description;
            private ItemRarity _rarity = ItemRarity.Common;
            private int _weight = 1;
            private int _damage;
            private float _attackSpeed = 1.0f;

            public Builder(string id, string name)
            {
                _id = id;
                _name = name;
                _description = $"{name} - {_rarity} оружие";
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

            public Builder WithDamage(int damage)
            {
                _damage = damage;
                return this;
            }

            public Builder WithAttackSpeed(float attackSpeed)
            {
                _attackSpeed = attackSpeed;
                return this;
            }

            public Weapon Build()
            {
                if (_damage <= 0)
                    throw new InvalidOperationException("Урон не может быть меньше 0");

                return new Weapon(_id, _name, _description, _rarity, _weight, _damage, _attackSpeed);
            }
        }
    }
}