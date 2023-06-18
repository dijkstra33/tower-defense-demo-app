using Core;
using UnityEngine;

namespace Game
{
    public class Tower : SingletonMoneBehaviour<Tower>
    {
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
    }
}