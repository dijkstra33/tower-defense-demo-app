using System;
using Core;
using Game.Weapons;
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

        public void SpendCurrency(int currency)
        {
            currencyAmount -= currency;
            currencyAmount = Math.Max(0, currencyAmount);
        }

        public AbstractWeapon[] GetWeapons()
        {
            return GetComponentsInChildren<AbstractWeapon>();
        }
    }
}