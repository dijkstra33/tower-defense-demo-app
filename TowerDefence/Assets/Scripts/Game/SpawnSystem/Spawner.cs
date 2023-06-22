using Core.ObjectPooling;
using UnityEngine;

namespace Game.SpawnSystem
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private Unit unitPrefab;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void Spawn(SpawnData spawnData)
        {
            var direction = (spawnData.TargetTransform.position - transform.position).normalized;
            
            var spawnedUnit = 
                ObjectPoolManager.Instance.GetObject(
                    unitPrefab, _transform.position, Quaternion.LookRotation(direction), 
                    unitPrefab.transform.localScale, _transform);

            spawnedUnit.MoveTo(spawnData.TargetTransform);
        }
    }
}