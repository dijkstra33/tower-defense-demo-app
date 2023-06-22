using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem.Buffs
{
    public class AddValueBuff : AbstractBuff
    {
        [SerializeField]
        private AttributeType buffedAttributeType;

        [SerializeField]
        private BuffHolderType buffHolderType;
        
        [SerializeField]
        private float value;
        
        public override AttributeType BuffedAttributeType => buffedAttributeType;
        public override BuffHolderType BuffHolderType => buffHolderType;
        public override float GetValue(AttackContext context) => value;
    }
}