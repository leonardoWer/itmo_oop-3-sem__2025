using Lab2.InventorySystem.State;

namespace Lab2.InventorySystem.Items.Weapon;

public class UpgradableWeapon : Weapon, IUpgradableItem
{
    public IUpgradeState UpgradeState { get; set; }

    public UpgradableWeapon(Weapon weapon) : base(
        weapon.Id, weapon.Name,
        weapon.Description, weapon.Rarity,
        weapon.Weight, weapon.Damage, weapon.AttackSpeed
    )
    {
        UpgradeState = new BaseUpgradeState();
    }

    public void ApplyUpgrade()
    {
        // Увеличиваем урон на 10% за каждый уровень
        var damageIncrease = (int)(Damage * 0.1);
        var newDamage = Damage + damageIncrease;
            
        Damage = newDamage;
    }
}