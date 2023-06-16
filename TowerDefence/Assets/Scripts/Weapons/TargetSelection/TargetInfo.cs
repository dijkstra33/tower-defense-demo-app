using UnityEngine;

namespace TowerDefence.TargetSelection
{
    public struct TargetInfo
    {
        public Health Health { get; }
        public Transform Transform { get; }

        public TargetInfo(Health health, Transform transform)
        {
            Health = health;
            Transform = transform;
        }
    }
}