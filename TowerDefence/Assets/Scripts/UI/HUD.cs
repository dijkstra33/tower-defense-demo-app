using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        public static HUD Instance { get; private set; }

        [SerializeField]
        private TMP_Text currencyAmountText;

        private int prevCurrencyAmount = -1;
        
        [SerializeField]
        private TMP_Text currencyIncomeText;
        
        private int prevCurrencyIncome = -1;

        [SerializeField]
        private Button restartButton;

        private void Awake()
        {
            restartButton.onClick.AddListener(RestartGameClicked);
            Instance = this;
        }

        private void Update()
        {
            var actualCurrencyAmount = Tower.Instance.CurrencyAmount;
            if (prevCurrencyAmount != actualCurrencyAmount)
            {
                prevCurrencyAmount = actualCurrencyAmount;
                currencyAmountText.text = actualCurrencyAmount.ToString();
            }

            var actualCurrencyPassiveIncome = Tower.Instance.CurrencyPassiveIncome;
            if (prevCurrencyIncome != actualCurrencyPassiveIncome)
            {
                prevCurrencyIncome = actualCurrencyPassiveIncome;
                currencyIncomeText.text = actualCurrencyPassiveIncome.ToString();
            }
        }

        public void ShowGameEndScreen()
        {
            // TODO: here will show game statistics and other stuff.
            restartButton.gameObject.SetActive(true);
        }

        private void RestartGameClicked()
        {
            GameManager.RestartLevel();
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}