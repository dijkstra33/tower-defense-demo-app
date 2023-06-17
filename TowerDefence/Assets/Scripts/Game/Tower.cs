using UnityEngine;

namespace Game
{
    public class Tower : MonoBehaviour
    {
        public static Tower Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}