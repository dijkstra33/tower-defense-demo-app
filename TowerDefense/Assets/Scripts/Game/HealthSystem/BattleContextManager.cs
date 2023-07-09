using System.Collections.Generic;
using Core;
using Game.WeaponSystem;

namespace Game.HealthSystem
{
    public class BattleContextManager : SingletonMonoBehaviour<BattleContextManager>
    {
        private readonly List<BattleContext> allBattleContexts = new();

        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDeath;
        }

        private void HandleUnitDeath(Unit diedUnit, AbstractWeapon killerWeapon, Health killerHealth)
        {
            var diedUnitWeapons = diedUnit.Weapons;
            foreach (var weapon in diedUnitWeapons)
            {
                foreach (var battleContext in allBattleContexts)
                {
                    battleContext.RemoveInfluenceOf(weapon);
                }
            }
        }

        public void OnDamageReceived(Health attackedHealth, AbstractWeapon attackerWeapon)
        {
            var attackedBattleContext = attackedHealth.GetComponent<BattleContext>();
            attackedBattleContext.RegisterHitBy(attackerWeapon);
        }

        public void Register(BattleContext battleContext)
        {
            allBattleContexts.Add(battleContext);
        }
    }
}