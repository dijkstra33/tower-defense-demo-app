using UnityEngine;

namespace Core
{
    public class SingletonMoneBehaviour<T> : MonoBehaviour where T : SingletonMoneBehaviour<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = (T)this;
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}