using Lab2.InventorySystem.Core.Enums;
using Lab2.InventorySystem.Core.Models;

namespace Lab2.InventorySystem.Items.Quest;

public class QuestItem(string id, string name, string description, ItemRarity rarity, int weight)
    : BaseItem(id, name, description, rarity, weight)
{
    public override ItemType Type => ItemType.Quest;

    public class Builder
    {
        private string _id;
        private string _name;
        private string _description;
        
        public Builder(string id, string name)
        {
            _id = id;
            _name = name;
            _description = $"{name} - квестовый предмет";
        }
        
        public Builder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public QuestItem Build()
        {
            return new QuestItem(_id, _name, _description, ItemRarity.Uncommon, 0);
        }
    }
}