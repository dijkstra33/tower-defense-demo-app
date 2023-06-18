using System;
using Core;
using Core.ObjectPooling;
using UnityEngine;

namespace Game.HealthSystem
{
    public class DeathManager : SingletonMoneBehaviour<DeathManager>
    {
        public event Action<Unit> OnUnitDeath;
        public event Action<Tower> OnTowerDeath;

        public void OnDeath(GameObject died)
        {
            var unit = died.GetComponent<Unit>();
            if (unit != null)
            {
                OnUnitDeath?.Invoke(unit);
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