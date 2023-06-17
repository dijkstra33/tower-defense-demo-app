using UnityEngine;

namespace Game.Spawning
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