namespace Lab2.InventorySystem.State
{
    // Паттерн State для улучшения предметов
    public class BaseUpgradeState : IUpgradeState
    {
        public int CurrentLevel { get; private set; } = 0;
        public int MaxLevel => 10;
        
        public bool CanUpgrade() => CurrentLevel < MaxLevel;
        
        public string Upgrade(IUpgradableItem item)
        {
            if (!CanUpgrade())
                return "Достигнут максимальный уровень улучшения";

            CurrentLevel++;
            item.ApplyUpgrade();
            return $"Предмет улучшен до уровня {CurrentLevel}";
        }

        public string GetStateInfo() => $"Уровень улучшения: {CurrentLevel}/{MaxLevel}";
    }
}