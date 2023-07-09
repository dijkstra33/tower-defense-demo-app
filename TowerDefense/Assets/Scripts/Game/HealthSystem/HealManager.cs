using Core;
using Game.AttributeSystem;
using Game.AttributeSystem.Buffs;
using Game.WeaponSystem;

namespace Game.HealthSystem
{
    public class HealManager : SingletonMonoBehaviour<HealManager>
    {
        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDeath;
        }

        public void TryHealAttacker(Health weaponOwnerHealth, AbstractAttributeOwner weaponAttributeOwner)
        {
            weaponOwnerHealth.ReceiveHeal(weaponAttributeOwner, AttributeType.HealOnHit);
        }

        private void HandleUnitDeath(Unit diedUnit, AbstractWeapon killerWeapon, Health killerHealth)
        {
            if (killerWeapon == null || killerHealth == null)
            {
                return;
            }

            killerHealth.ReceiveHeal(killerWeapon.AttributeOwner, AttributeType.HealOnKill);
        }
    }
}