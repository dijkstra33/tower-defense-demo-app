using System;
using Core;
using Core.ObjectPooling;
using Game.AttributeSystem.Buffs;
using Game.Weapons;
using UnityEngine;

namespace Game.HealthSystem
{
    public class DeathManager : SingletonMoneBehaviour<DeathManager>
    {
        public delegate void OnUnitDeathAction(Unit diedUnit, AbstractWeapon killerWeapon, Health killerHealth);

        public event OnUnitDeathAction OnUnitDeath;
        public event Action<Tower> OnTowerDeath;

        public void OnDeath(GameObject died, AbstractWeapon killerWeapon, Health attackerHealth)
        {
            var unit = died.GetComponent<Unit>();
            if (unit != null)
            {
                OnUnitDeath?.Invoke(unit, killerWeapon, attackerHealth);
            }

            var tower = died.GetComponent<Tower>();
            if (tower != null)
            {
                OnTowerDeath?.Invoke(tower);
            }

            var poolable = died.GetComponent<Poolable>();
            if (poolable != null)
            {
                poolable.ReleaseObject();
            }
        }
    }
}