using System.Collections.Generic;
using Core;
using Game.AttributeSystem.Buffs;

namespace Game.HealthSystem
{
    public class BattleContextManager : SingletonMoneBehaviour<BattleContextManager>
    {
        private Dictionary<Health, BattleContext> battleContextByHealth = new();
        private Dictionary<BuffHolder, BattleContext> battleContextByBuffHolder = new();

        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDeath;
        }

        private void HandleUnitDeath(Unit diedUnit, BuffHolder killerWeaponBuffHolder, Health killerHealth)
        {
            var diedUnitWeapons = diedUnit.Weapons;
            foreach (var weapon in diedUnitWeapons)
            {
                var weaponBuffHolder = weapon.GetComponent<BuffHolder>();
                foreach (var battleContext in battleContextByHealth.Values)
                {
                    battleContext.RemoveInfluenceOf(weaponBuffHolder);
                }
            }
        }

        public void OnDamageReceived(Health attackedHealth, BuffHolder attackerBuffHolder)
        {
            var attackedBattleContext = GetBattleContextBy(attackedHealth);
            attackedBattleContext.RegisterHitBy(attackerBuffHolder);
        }

        public void Register(BattleContext battleContext)
        {
            var health = battleContext.GetComponent<Health>();
            if (health != null)
            {
                battleContextByHealth.Add(health, battleContext);
            }

            var buffHolder = battleContext.GetComponent<BuffHolder>();
            if (buffHolder != null)
            {
                battleContextByBuffHolder.Add(buffHolder, battleContext);
            }
        }

        public BattleContext GetBattleContextBy(Health health)
        {
            return battleContextByHealth.TryGetValue(health, out var battleContext) ? battleContext : null;
        }
    }
}