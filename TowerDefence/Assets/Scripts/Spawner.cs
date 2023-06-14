using UnityEngine;

namespace TowerDefence
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject unitPrefab;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void Spawn(SpawnData spawnData)
        {
            var direction = (spawnData.TargetTransform.position - transform.position).normalized;
            // TODO: use some sort of object pool.
            Instantiate(unitPrefab, _transform.position, Quaternion.LookRotation(direction), _transform);
        }
    }
}