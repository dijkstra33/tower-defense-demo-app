using UnityEngine;

namespace TowerDefence
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
            // TODO: use some sort of object pool.
            var spawnedUnit = Instantiate(unitPrefab, _transform.position, Quaternion.LookRotation(direction), _transform);
            spawnedUnit.SetData(spawnData.TargetTransform);
        }
    }
}