namespace Lab2.InventorySystem.State
{
    public interface IUpgradableItem
    {
        IUpgradeState UpgradeState { get; set; }
        void ApplyUpgrade();
    }
}