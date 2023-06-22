using System.Collections.Generic;
using UnityEngine;

namespace Core.ObjectPooling
{
    public class ObjectPool
    {
        private Queue<GameObject> unusedObjects = new();
        private readonly Poolable poolablePrefab;
        private readonly Transform unusedObjectParent;

        public ObjectPool(Poolable poolablePrefab, Transform unusedObjectParent)
        {
            this.unusedObjectParent = unusedObjectParent;
            this.poolablePrefab = poolablePrefab;
            for (int i = 0; i < poolablePrefab.InitialPoolSize; i++)
            {
                var unusedObject = poolablePrefab.InstantiateSelf(unusedObjectParent);
                unusedObject.SetActive(false);
                unusedObjects.Enqueue(unusedObject);
            }
        }

        public T GetObject<T>(Vector3 position, Quaternion rotation, Vector3? scale, Transform parent = null) 
            where T : MonoBehaviour
        {
            var objectParent = parent != null ? parent : unusedObjectParent;
            GameObject @object;

            if (unusedObjects.TryDequeue(out @object))
            {
                @object.transform.parent = objectParent;
            }
            else
            {
                @object = poolablePrefab.InstantiateSelf(objectParent);
            }

            @object.transform.position = position;
            @object.transform.rotation = rotation;
            @object.transform.localScale = scale ?? Vector3.one;

            var resettableArray = @object.GetComponentsInChildren<IResettable>();
            foreach (var resettable in resettableArray)
            {
                resettable.Reset();
            }
            
            @object.SetActive(true);
            
            return @object.GetComponent<T>();
        }

        public void ReleaseObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.transform.parent = unusedObjectParent;
            unusedObjects.Enqueue(gameObject);
        }
    }
}