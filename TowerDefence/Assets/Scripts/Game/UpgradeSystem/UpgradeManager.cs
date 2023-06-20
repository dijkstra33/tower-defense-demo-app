using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Game.Weapons;
using UI;
using UnityEngine;

namespace Game.UpgradeSystem
{
    public class UpgradeManager : SingletonMoneBehaviour<UpgradeManager>
    {
        private const string UPGRADES_PATH = "ScriptableObjects/Upgrades/";
        
        [SerializeField]
        private float upgradeSystemStartDelay;
        
        [SerializeField]
        private float upgradeAutoRerollInterval;

        public int UpgradesPerReroll => upgradesPerReroll;
        [SerializeField]
        private int upgradesPerReroll;

        private Upgrade[] allUpgrades;
        private Upgrade[] availableUpgrades;

        public float TimeUntilAutoReroll => timeUntilAutoReroll;
        private float timeUntilAutoReroll;

        public float AutoRerollCycleDuration => autoRerollCycleDuration;
        private float autoRerollCycleDuration;

        private List<Upgrade> activeUpgrades = new();
        
        private readonly System.Random random = new();

        protected override void Awake()
        {
            base.Awake();
            allUpgrades = Resources.LoadAll<Upgrade>(UPGRADES_PATH);
            timeUntilAutoReroll = upgradeSystemStartDelay;
            autoRerollCycleDuration = upgradeSystemStartDelay;
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
                timeUntilAutoReroll = upgradeAutoRerollInterval;
                autoRerollCycleDuration = upgradeAutoRerollInterval;
            }
        }

        private void RerollUpgrades()
        {
            availableUpgrades = allUpgrades
                .OrderBy(x => random.Next())
                .Take(upgradesPerReroll)
                .ToArray();

            HUD.Instance.SetAvailableUpgrades(availableUpgrades);
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
            activeUpgrades.Add(upgrade);
            availableUpgrades[Array.IndexOf(availableUpgrades, upgrade)] = null;
            HUD.Instance.SetAvailableUpgrades(availableUpgrades);
        }

        public float GetUpgradeValue(UpgradeType upgradeType, AbstractWeapon weapon)
        {
            if (!weapon.IsUpgradable)
            {
                return 0f;
            }
            
            var value = 0f;
            foreach (var activeUpgrade in activeUpgrades)
            {
                if (!activeUpgrade.IsApplicable(upgradeType, weapon.WeaponType))
                {
                    continue;
                }

                value += activeUpgrade.GetUpgradeValue();
            }

            return value;
        }
    }
}
