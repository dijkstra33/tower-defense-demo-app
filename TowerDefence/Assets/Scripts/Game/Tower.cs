using System;
using Core;
using Game.AttributeSystem;
using Game.HealthSystem;
using Game.Weapons;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TowerAttributeOwner), typeof(BattleContext))]
    public class Tower : SingletonMoneBehaviour<Tower>
    {
        public int CurrencyAmount => currencyAmount;
        [SerializeField]
        private int currencyAmount;

        public AbstractAttributeOwner AttributeOwner => attributeOwner;
        private AbstractAttributeOwner attributeOwner;

        private AbstractWeapon[] weapons;
        public AbstractWeapon[] Weapons => weapons;

        protected override void Awake()
        {
            base.Awake();
            attributeOwner = GetComponent<AbstractAttributeOwner>();
            weapons = GetComponentsInChildren<AbstractWeapon>();
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