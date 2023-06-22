using Game.Weapons;
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
                    return buffHolder.GetBuffedValue(attackRange, AttributeType.AttackRange, attackContext);
                case AttributeType.Damage:
                    return buffHolder.GetBuffedValue(attackDamage, AttributeType.Damage, attackContext);
                case AttributeType.AttackInterval:
                    return buffHolder.GetBuffedValue(attackInterval, AttributeType.AttackInterval, attackContext);
            }
            return base.GetValue(attributeType, attackContext);
        }
    }
}