using Game;
using Game.AttributeSystem;
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
        private Tower tower;

        protected void Start()
        {
            tower = Tower.Instance;
        }

        private void Update()
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }

            var actualCurrencyAmount = tower.CurrencyAmount;
            if (prevCurrencyAmount != actualCurrencyAmount)
            {
                prevCurrencyAmount = actualCurrencyAmount;
                currencyAmountText.text = actualCurrencyAmount.ToString();
            }

            var actualCurrencyPassiveIncome = (int)tower.AttributeOwner.GetValue(AttributeType.CurrencyPassiveIncome);
            if (prevCurrencyIncome != actualCurrencyPassiveIncome)
            {
                prevCurrencyIncome = actualCurrencyPassiveIncome;
                currencyIncomeText.text = actualCurrencyPassiveIncome.ToString();
            }

            var actualKillCurrencyBonus = (int)tower.AttributeOwner.GetValue(AttributeType.KillCurrencyBonus);
            if (prevKillCurrencyBonus != actualKillCurrencyBonus)
            {
                prevKillCurrencyBonus = actualKillCurrencyBonus;
                killCurrencyBonusText.text = actualKillCurrencyBonus.ToString();
            }

            var actualArmor = (int)tower.AttributeOwner.GetValue(AttributeType.Armor);
            if (prevArmor != actualArmor)
            {
                prevArmor = actualArmor;
                armorText.text = actualArmor.ToString();
            }
        }
    }
}