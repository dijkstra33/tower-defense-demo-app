using Core;
using Game;
using Game.UpgradeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : SingletonMoneBehaviour<HUD>
    {
        [SerializeField]
        private UpgradePanel upgradePanel;

        [SerializeField]
        private Button restartButton;

        protected override void Awake()
        {
            base.Awake();
            restartButton.onClick.AddListener(RestartGameClicked);
        }
        
        public void SetAvailableUpgrades(Upgrade[] availableUpgrades)
        {
            upgradePanel.SetData(availableUpgrades);
        }

        public void ShowGameOverScreen()
        {
            // TODO: here will show game statistics and other stuff.
            restartButton.gameObject.SetActive(true);
        }

        private void RestartGameClicked()
        {
            GameManager.RestartLevel();
        }
    }
}