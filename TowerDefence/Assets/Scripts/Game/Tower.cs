using System;
using Core;
using Game.AttributeSystem;
using Game.Weapons;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TowerAttributeOwner))]
    public class Tower : SingletonMoneBehaviour<Tower>
    {
        public int CurrencyAmount => currencyAmount;
        [SerializeField]
        private int currencyAmount;

        public AbstractAttributeOwner AttributeOwner => attributeOwner;
        private AbstractAttributeOwner attributeOwner;

        protected override void Awake()
        {
            base.Awake();
            attributeOwner = GetComponent<AbstractAttributeOwner>();
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