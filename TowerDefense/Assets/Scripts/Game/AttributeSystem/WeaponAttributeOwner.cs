using Game.WeaponSystem;
using UnityEngine;

namespace Game.AttributeSystem
{
    public class WeaponAttributeOwner : AbstractAttributeOwner
    {
        [SerializeField]
        private float attackDamage;

        [SerializeField]
        private float attackInterval;

        [SerializeField]
        private float attackRange;

        public override float GetValue(AttributeType attributeType, AttackContext attackContext = null)
        {
            switch (attributeType)
            {
                case AttributeType.AttackRange:
                    return BuffOwner.GetBuffedValue(attackRange, AttributeType.AttackRange, attackContext);
                case AttributeType.Damage:
                    return BuffOwner.GetBuffedValue(attackDamage, AttributeType.Damage, attackContext);
                case AttributeType.AttackInterval:
                    return BuffOwner.GetBuffedValue(attackInterval, AttributeType.AttackInterval, attackContext);
            }
            return base.GetValue(attributeType, attackContext);
        }
    }
}