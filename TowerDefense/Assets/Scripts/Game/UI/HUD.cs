﻿using Core;
using Game.AttributeSystem.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HUD : SingletonMonoBehaviour<HUD>
    {
        [SerializeField]
        private UpgradesPanel upgradesPanel;

        [SerializeField]
        private Button restartButton;

        protected override void Awake()
        {
            base.Awake();
            restartButton.onClick.AddListener(RestartGameClicked);
        }

        private void Start()
        {
            UpgradeManager.Instance.OnAvailableUpgradesChanged += HandleAvailableUpgradesChanged;
            GameManager.Instance.OnGameOver += HandleGameOver;
        }

        private void HandleAvailableUpgradesChanged(Upgrade[] availableUpgrades)
        {
            upgradesPanel.SetData(availableUpgrades);
        }

        private void HandleGameOver()
        {
            restartButton.gameObject.SetActive(true);
        }

        private void RestartGameClicked()
        {
            GameManager.RestartLevel();
        }
    }
}