using Game.WeaponSystem;
using UnityEngine;

namespace Game.AttributeSystem.Buffs
{
    public class AddValueBuff : AbstractBuff
    {
        [SerializeField]
        private AttributeType buffedAttributeType;
        
        [SerializeField]
        private float value;
        
        public override AttributeType BuffedAttributeType => buffedAttributeType;
        public override float GetValue(AttackContext context) => value;
    }
}