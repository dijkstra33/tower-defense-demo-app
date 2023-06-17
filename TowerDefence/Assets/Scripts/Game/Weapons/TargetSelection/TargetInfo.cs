using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public struct TargetInfo
    {
        public Health.Health Health { get; }
        public Transform Transform { get; }

        public TargetInfo(Health.Health health, Transform transform)
        {
            Health = health;
            Transform = transform;
        }
    }
}