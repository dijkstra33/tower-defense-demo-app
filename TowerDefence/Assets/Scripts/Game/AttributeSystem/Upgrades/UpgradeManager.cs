using System;
using System.Linq;
using Core;
using Game.AttributeSystem.Buffs;
using UnityEngine;

namespace Game.AttributeSystem.Upgrades
{
    public class UpgradeManager : SingletonMoneBehaviour<UpgradeManager>
    {
        public event Action<Upgrade[]> OnAvailableUpgradesChanged; 
        
        private const string UPGRADES_PATH = "ScriptableObjects/Upgrades/";
        
        [SerializeField]
        private float upgradeSystemStartDelay;
        
        [SerializeField]
        private float upgradeAutoRerollInterval;

        public int UpgradesPerReroll => upgradesPerReroll;
        [SerializeField]
        private int upgradesPerReroll;

        public int RerollPrice => rerollPrice;
        private int rerollPrice;
        
        [SerializeField]
        private int baseRerollPrice;

        [SerializeField]
        private int rerollPriceIncrease;

        private Upgrade[] allUpgrades;
        private Upgrade[] availableUpgrades;

        public float TimeUntilAutoReroll => timeUntilAutoReroll;
        private float timeUntilAutoReroll;

        public float AutoRerollCycleDuration => autoRerollCycleDuration;
        private float autoRerollCycleDuration;

        private BuffHolder[] buffHolders;
        
        private readonly System.Random random = new();

        protected override void Awake()
        {
            base.Awake();
            buffHolders = Tower.Instance.GetComponentsInChildren<BuffHolder>();
            allUpgrades = Resources.LoadAll<Upgrade>(UPGRADES_PATH);
            timeUntilAutoReroll = upgradeSystemStartDelay;
            autoRerollCycleDuration = upgradeSystemStartDelay;
            rerollPrice = baseRerollPrice;
        }
        private void Update()
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }
            
            timeUntilAutoReroll -= Time.deltaTime;
            if (timeUntilAutoReroll <= 0f)
            {
                RerollUpgrades();
            }
        }

        private void RerollUpgrades()
        {
            availableUpgrades = allUpgrades
                .Where(x => !x.IsUseless())
                .OrderBy(x => random.Next())
                .Take(upgradesPerReroll)
                .ToArray();

            OnAvailableUpgradesChanged?.Invoke(availableUpgrades);
            
            timeUntilAutoReroll = upgradeAutoRerollInterval;
            autoRerollCycleDuration = upgradeAutoRerollInterval;
        }

        public void BuyUpgrade(Upgrade upgrade)
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }
            
            var isUpgradeAffordable = Tower.Instance.CurrencyAmount >= upgrade.Price;
            if (upgrade == null || !isUpgradeAffordable)
            {
                return;
            }

            Tower.Instance.SpendCurrency(upgrade.Price);
            foreach (var buffHolder in buffHolders)
            {
                if (upgrade.BuffHolderType == buffHolder.Type)
                {
                    buffHolder.ApplyBuff(upgrade.Buff);
                }
            }
            availableUpgrades[Array.IndexOf(availableUpgrades, upgrade)] = null;
            OnAvailableUpgradesChanged?.Invoke(availableUpgrades);
        }

        public void BuyReroll()
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }
            
            var isRerollAffordable = Tower.Instance.CurrencyAmount >= rerollPrice;
            if (!isRerollAffordable)
            {
                return;
            }

            Tower.Instance.SpendCurrency(rerollPrice);
            rerollPrice += rerollPriceIncrease;

            RerollUpgrades();
        }
    }
}
