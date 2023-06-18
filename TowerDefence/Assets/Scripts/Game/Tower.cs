using UnityEngine;

namespace Game
{
    public class Tower : MonoBehaviour
    {
        public static Tower Instance { get; private set; }

        [SerializeField]
        private int currencyValue;

        public void ReceiveCurrency(int currency)
        {
            currencyValue += currency;
        }
        
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