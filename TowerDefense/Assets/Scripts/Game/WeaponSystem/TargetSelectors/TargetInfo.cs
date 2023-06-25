using Game.HealthSystem;
using UnityEngine;

namespace Game.WeaponSystem.TargetSelectors
{
    public struct TargetInfo
    {
        public Health Health { get; }
        public Transform Transform { get; }
        public BattleContext BattleContext { get; }

        public TargetInfo(Health health, Transform transform)
        {
            Health = health;
            Transform = transform;
            BattleContext = health.GetComponent<BattleContext>();
        }
    }
}