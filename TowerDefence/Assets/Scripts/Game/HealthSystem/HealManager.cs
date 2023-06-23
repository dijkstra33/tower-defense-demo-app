using System;
using Core;
using Game.AttributeSystem;
using Game.AttributeSystem.Buffs;

namespace Game.HealthSystem
{
    public class HealManager : SingletonMoneBehaviour<HealManager>
    {
        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDeath;
        }

        public void TryHealAttacker(Health weaponOwnerHealth, AbstractAttributeOwner weaponAttributeOwner)
        {
            weaponOwnerHealth.ReceiveHeal(weaponAttributeOwner, AttributeType.HealOnHit);
        }

        private void HandleUnitDeath(Unit diedUnit, BuffHolder weaponBuffHolder, Health killerHealth)
        {
            if (weaponBuffHolder == null || killerHealth == null)
            {
                return;
            }

            var weaponAttributeOwner = weaponBuffHolder.GetComponent<AbstractAttributeOwner>();
            killerHealth.ReceiveHeal(weaponAttributeOwner, AttributeType.HealOnKill);
        }
    }
}