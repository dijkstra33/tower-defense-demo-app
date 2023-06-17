using UnityEngine;

namespace Core.ObjectPooling
{
    public class Poolable : MonoBehaviour
    {
        public int InitialPoolSize => initialPoolSize;
        [SerializeField]
        private int initialPoolSize;

        public int PoolKey { get; private set; }

        public GameObject InstantiateSelf(Transform parent)
        {
            var newObject = Instantiate(gameObject, Vector3.zero, Quaternion.identity, parent);
            var newObjectPoolable = newObject.GetComponent<Poolable>();
            newObjectPoolable.PoolKey = gameObject.GetInstanceID();
            return newObject;
        }

        public void ReleaseObject()
        {
            ObjectPoolManager.Instance.ReleaseObject(this);
        }
    }
}