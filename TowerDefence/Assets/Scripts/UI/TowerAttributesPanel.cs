using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TowerAttributesPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text currencyAmountText;

        private int prevCurrencyAmount = -1;
        
        [SerializeField]
        private TMP_Text currencyIncomeText;
        
        private int prevCurrencyIncome = -1;
        
        [SerializeField]
        private TMP_Text killCurrencyBonusText;
        
        private int prevkillCurrencyBonus = -1;

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

            var actualCurrencyPassiveIncome = Tower.Instance.GetCurrencyPassiveIncome();
            if (prevCurrencyIncome != actualCurrencyPassiveIncome)
            {
                prevCurrencyIncome = actualCurrencyPassiveIncome;
                currencyIncomeText.text = actualCurrencyPassiveIncome.ToString();
            }

            var actualKillCurrencyBonus = Tower.Instance.GetKillCurrencyBonus();
            if (prevkillCurrencyBonus != actualKillCurrencyBonus)
            {
                prevkillCurrencyBonus = actualKillCurrencyBonus;
                killCurrencyBonusText.text = actualKillCurrencyBonus.ToString();
            }
        }
    }
}