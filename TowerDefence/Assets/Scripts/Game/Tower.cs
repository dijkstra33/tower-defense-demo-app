using System;
using Core;
using Game.UpgradeSystem;
using Game.Weapons;
using UnityEngine;

namespace Game
{
    public class Tower : SingletonMoneBehaviour<Tower>
    {
        public int CurrencyAmount => currencyAmount;
        [SerializeField]
        private int currencyAmount;

        public int GetCurrencyPassiveIncome() => 
            baseCurrencyPassiveIncome + (int)UpgradeManager.Instance.GetUpgradeValue(UpgradeType.CurrencyPassiveIncome);

        [SerializeField]
        private int baseCurrencyPassiveIncome;
        
        public int GetKillCurrencyBonus()
        {
            return (int)UpgradeManager.Instance.GetUpgradeValue(UpgradeType.KillCurrencyBonus);
        }

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