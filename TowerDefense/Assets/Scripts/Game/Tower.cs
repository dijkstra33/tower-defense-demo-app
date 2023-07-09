using System;
using Core;
using Game.AttributeSystem;
using Game.HealthSystem;
using Game.WeaponSystem;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TowerAttributeOwner), typeof(BattleContext))]
    public class Tower : SingletonMonoBehaviour<Tower>
    {
        public int CurrencyAmount => currencyAmount;
        private int currencyAmount;
        
        [SerializeField] 
        private int baseCurrencyAmount;

        public AbstractAttributeOwner AttributeOwner => attributeOwner;
        private AbstractAttributeOwner attributeOwner;

        private AbstractWeapon[] weapons;
        public AbstractWeapon[] Weapons => weapons;

        protected override void Awake()
        {
            base.Awake();
            attributeOwner = GetComponent<AbstractAttributeOwner>();
            weapons = GetComponentsInChildren<AbstractWeapon>();
            currencyAmount = baseCurrencyAmount;
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
    }
}