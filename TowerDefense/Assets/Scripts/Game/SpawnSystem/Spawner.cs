using System;
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

        public void SpawnUnit(SpawnData spawnData)
        {
            Spawn(unitPrefab, spawnedUnit => spawnedUnit.MoveTo(spawnData.TargetTransform), spawnData);
        }

        public void Spawn<T>(T prefab, Action<T> init, SpawnData spawnData) where T : MonoBehaviour
        {
            var direction = (spawnData.TargetTransform.position - transform.position).normalized;
            
            var spawnedObject = 
                ObjectPoolManager.Instance.GetObject(
                    prefab, cachedTransform.position, Quaternion.LookRotation(direction), 
                    prefab.transform.localScale, cachedTransform);
            init(spawnedObject);
        }
    }
}