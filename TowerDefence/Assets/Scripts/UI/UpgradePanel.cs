using Game.UpgradeSystem;
using UnityEngine;

namespace UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField]
        private UpgradeView upgradeViewPrefab;

        [SerializeField]
        private Transform upgradeViewsRoot;

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