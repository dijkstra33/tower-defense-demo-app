using Game;
using Game.HealthSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TowerAttributesPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text currencyAmountText;

        // TODO: check with profiler - is it worth it?
        private int prevCurrencyAmount = -1;
        
        [SerializeField]
        private TMP_Text currencyIncomeText;
        
        private int prevCurrencyIncome = -1;
        
        [SerializeField]
        private TMP_Text killCurrencyBonusText;
        
        private int prevKillCurrencyBonus = -1;
        
        [SerializeField]
        private TMP_Text armorText;
        
        private int prevArmor = -1;

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
            if (prevKillCurrencyBonus != actualKillCurrencyBonus)
            {
                prevKillCurrencyBonus = actualKillCurrencyBonus;
                killCurrencyBonusText.text = actualKillCurrencyBonus.ToString();
            }

            var armored = Tower.Instance.GetComponent<Armored>();
            var actualArmor = armored?.GetArmor() ?? 0;
            if (prevArmor != actualArmor)
            {
                prevArmor = actualArmor;
                armorText.text = actualArmor.ToString();
            }
        }
    }
}