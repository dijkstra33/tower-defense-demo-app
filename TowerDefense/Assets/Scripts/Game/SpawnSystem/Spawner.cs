using Core.ObjectPooling;
using UnityEngine;

namespace Game.SpawnSystem
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private Unit unitPrefab;

        private Transform cachedTransform;

        private void Start()
        {
            cachedTransform = transform;
        }

        public void Spawn(SpawnData spawnData)
        {
            var direction = (spawnData.TargetTransform.position - transform.position).normalized;
            
            var spawnedUnit = 
                ObjectPoolManager.Instance.GetObject(
                    unitPrefab, cachedTransform.position, Quaternion.LookRotation(direction), 
                    unitPrefab.transform.localScale, cachedTransform);

            spawnedUnit.MoveTo(spawnData.TargetTransform);
        }
    }
}