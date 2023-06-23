using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem.Buffs
{
    public class AddDamagePerEachTargetBuff : AbstractBuff
    {
        [SerializeField]
        private float damagePerEachTarget;
        
        public override AttributeType BuffedAttributeType => AttributeType.Damage;
        public override float GetValue(AttackContext context)
        {
            var targetsCount = context?.Targets?.Length ?? 0;
            return targetsCount * damagePerEachTarget;
        }

        public override float GetValueForText(AttackContext context) => damagePerEachTarget;
    }
}