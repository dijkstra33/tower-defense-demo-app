using UnityEngine;

namespace TowerDefence
{
    public class SpawnData
    {
        public Transform TargetTransform { get; }

        public SpawnData(Transform targetTransform)
        {
            TargetTransform = targetTransform;
        }
    }
}