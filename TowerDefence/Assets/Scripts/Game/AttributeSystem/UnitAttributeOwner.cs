using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem
{
    public class UnitAttributeOwner : AbstractAttributeOwner
    {
        [SerializeField]
        private int baseKillCurrencyBonus;
        
        public override float GetValue(AttributeType attributeType, AttackContext attackContext = null)
        {
            switch (attributeType)
            {
                case AttributeType.KillCurrencyBonus:
                    return (int)buffHolder.GetBuffedValue(baseKillCurrencyBonus, attributeType, attackContext);
            }

            return base.GetValue(attributeType, attackContext);
        }
    }
}