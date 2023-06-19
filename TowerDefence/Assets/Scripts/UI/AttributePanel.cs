using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AttributePanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text currencyAmountText;

        private int prevCurrencyAmount = -1;
        
        [SerializeField]
        private TMP_Text currencyIncomeText;
        
        private int prevCurrencyIncome = -1;

        private void Update()
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }
            
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
    }
}