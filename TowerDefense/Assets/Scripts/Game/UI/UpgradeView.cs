using Game.AttributeSystem.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private TMP_Text priceLabel;

        [SerializeField]
        private TMP_Text valueLabel;

        private Upgrade upgrade;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        public void SetData(Upgrade upgrade)
        {
            this.upgrade = upgrade;
            gameObject.SetActive(upgrade != null);
            if (upgrade == null)
            {
                return;
            }
            
            icon.sprite = upgrade.Sprite;
            priceLabel.text = upgrade.Price.ToString();
            valueLabel.text = upgrade.GetValueText();
        }
        
        private void OnClick()
        {
            UpgradeManager.Instance.BuyUpgrade(upgrade);
        }
    }
}