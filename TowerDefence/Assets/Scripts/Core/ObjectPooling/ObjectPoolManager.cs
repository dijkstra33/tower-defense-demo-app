using System.Collections.Generic;
using UnityEngine;

namespace Core.ObjectPooling
{
    public class ObjectPoolManager : SingletonMoneBehaviour<ObjectPoolManager>
    {
        private Dictionary<int, ObjectPool> objectPools = new();
        private Transform _transform;

        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
        }

        public T GetObject<T>(T poolablePrefab, Vector3 position, Quaternion rotation, Vector3? scale = null, Transform parent = null)
            where T : MonoBehaviour
        {
            var poolable = poolablePrefab.GetComponent<Poolable>();
            if (poolable == null)
            {
                Debug.LogError($"Prefab must have {nameof(Poolable)} component to be instantiated by {nameof(ObjectPoolManager)}!");
                return null;
            }

            var objectPool = GetObjectPool(poolable);
            return objectPool.GetObject<T>(position, rotation, scale, parent);
        }
        
        private ObjectPool GetObjectPool(Poolable poolable)
        {
            var instanceId = poolable.gameObject.GetInstanceID();
            if (!objectPools.ContainsKey(instanceId))
            {
                objectPools.Add(instanceId, new ObjectPool(poolable, _transform));
            }

            return objectPools[instanceId];
        }

        public void ReleaseObject(Poolable poolable)
        {
            var objectPool = GetObjectPool(poolable.PoolKey);
            objectPool.ReleaseObject(poolable.gameObject);
        }

        private ObjectPool GetObjectPool(int poolKey)
        {
            if (!objectPools.TryGetValue(poolKey, out var objectPool))
            {
                Debug.LogError($"There is no object pool with key = {poolKey}!");
                return null;
            }

            return objectPool;
        }
    }
}