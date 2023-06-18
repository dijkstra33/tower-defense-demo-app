using Core;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : SingletonMoneBehaviour<HUD>
    {
        [SerializeField]
        private TMP_Text currencyAmountText;

        private int prevCurrencyAmount = -1;
        
        [SerializeField]
        private TMP_Text currencyIncomeText;
        
        private int prevCurrencyIncome = -1;

        [SerializeField]
        private Button restartButton;

        protected override void Awake()
        {
            base.Awake();
            restartButton.onClick.AddListener(RestartGameClicked);
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