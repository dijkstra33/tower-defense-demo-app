using UnityEngine;

namespace Game
{
    public class Tower : MonoBehaviour
    {
        public static Tower Instance { get; private set; }

        public int CurrencyAmount => currencyAmount;
        [SerializeField]
        private int currencyAmount;

        public int CurrencyPassiveIncome => baseCurrencyPassiveIncome /* + bonuses */;
        [SerializeField]
        private int baseCurrencyPassiveIncome;

        public void ReceiveCurrency(int currency)
        {
            currencyAmount += currency;
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