namespace Lab2.InventorySystem.Core.Interfaces;

public interface IInventoryManager
{
    IInventory Inventory { get; }
    string UseItem(string itemId);
    string UpgradeItem(string itemId);
    string DisplayInventory();
    string DisplayEquippedItems();
    bool CombineItems(string sourceItemId, string targetItemId);
}