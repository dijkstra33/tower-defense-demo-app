using Game;
using Game.UpgradeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradesPanel : MonoBehaviour
    {
        [SerializeField]
        private UpgradeView upgradeViewPrefab;

        [SerializeField]
        private Transform upgradeViewsRoot;

        [SerializeField]
        private Image autoRerollProgressBarImage;

        private UpgradeView[] upgradeViews;

        private void Start()
        {
            upgradeViews = new UpgradeView[UpgradeManager.Instance.UpgradesPerReroll];
            for (int i = 0; i < upgradeViews.Length; i++)
            {
                upgradeViews[i] = Instantiate(upgradeViewPrefab, upgradeViewsRoot);
                upgradeViews[i].SetData(null);
            }
        }

        private void Update()
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }
            
            autoRerollProgressBarImage.fillAmount = 
                UpgradeManager.Instance.TimeUntilAutoReroll / UpgradeManager.Instance.AutoRerollCycleDuration;
        }

        public void SetData(Upgrade[] upgrades)
        {
            for (int i = 0; i < upgradeViews.Length; i++)
            {
                var upgrade = (i < upgrades.Length) ? upgrades[i] : null;
                upgradeViews[i].SetData(upgrade);
            }
        }
    }
}