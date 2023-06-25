using Game.WeaponSystem;
using UnityEngine;

namespace Game.AttributeSystem
{
    public class TowerAttributeOwner : AbstractAttributeOwner
    {
        [SerializeField]
        private int baseCurrencyPassiveIncome;
        
        [SerializeField]
        private int baseKillCurrencyBonus;

        public override float GetValue(AttributeType attributeType, AttackContext attackContext = null)
        {
            switch (attributeType)
            {
                case AttributeType.Armor:
                    return GetArmor();
                case AttributeType.CurrencyPassiveIncome:
                    return (int)BuffOwner.GetBuffedValue(baseCurrencyPassiveIncome, attributeType, attackContext);
                case AttributeType.KillCurrencyBonus:
                    return (int)BuffOwner.GetBuffedValue(baseKillCurrencyBonus, attributeType, attackContext);
            }
            return base.GetValue(attributeType, attackContext);
        }
    }
}