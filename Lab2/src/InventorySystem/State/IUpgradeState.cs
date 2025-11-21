namespace Lab2.InventorySystem.State
{
    public interface IUpgradeState
    {
        int CurrentLevel { get; }
        int MaxLevel { get; }
        bool CanUpgrade();
        string Upgrade(IUpgradableItem item);
        string GetStateInfo();
    }
}