using Game.AttributeSystem.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UpgradesPanel : MonoBehaviour
    {
        [SerializeField]
        private UpgradeView upgradeViewPrefab;

        [SerializeField]
        private Transform upgradeViewsRoot;

        [SerializeField]
        private Image autoRerollProgressBarImage;

        [SerializeField]
        private Button rerollButton;
        
        [SerializeField]
        private TMP_Text rerollPriceText;

        private UpgradeView[] upgradeViews;

        private void Start()
        {
            upgradeViews = new UpgradeView[UpgradeManager.Instance.UpgradesPerReroll];
            for (int i = 0; i < upgradeViews.Length; i++)
            {
                upgradeViews[i] = Instantiate(upgradeViewPrefab, upgradeViewsRoot);
                upgradeViews[i].SetData(null);
            }
            rerollButton.onClick.AddListener(HandleRerollClick);
            rerollPriceText.text = UpgradeManager.Instance.RerollPrice.ToString();
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
        
        private void HandleRerollClick()
        {
            UpgradeManager.Instance.BuyReroll();
            rerollPriceText.text = UpgradeManager.Instance.RerollPrice.ToString();
        }
    }
}