using UnityEngine;

namespace Game.SpawnSystem
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