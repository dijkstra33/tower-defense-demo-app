using Game.UpgradeSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private TMP_Text priceLabel;

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
        }
        
        private void OnClick()
        {
            UpgradeManager.Instance.BuyUpgrade(upgrade);
        }
    }
}