using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem.Buffs
{
    public class AddDamagePerEachAttackOnSameTargetBuff : AbstractBuff
    {
        [SerializeField]
        private float damagePerEachHitOnSameTarget;
        
        public override AttributeType BuffedAttributeType => AttributeType.Damage;

        public override float GetValue(AttackContext context)
        {
            var battleContext = context.Target.BattleContext;
            var targetHitsCount = 
                battleContext != null 
                    ? battleContext.GetHitsCountBy(context.AttackerBuffHolder)
                    : 0;

            return targetHitsCount * damagePerEachHitOnSameTarget;
        }

        public override float GetValueForText(AttackContext context) => damagePerEachHitOnSameTarget;
    }
}