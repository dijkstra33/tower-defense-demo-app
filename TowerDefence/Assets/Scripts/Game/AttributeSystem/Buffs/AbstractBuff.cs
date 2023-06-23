using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem.Buffs
{
    public abstract class AbstractBuff : ScriptableObject
    {
        public BuffApplicationType ApplicationType => applicationType;
        [SerializeField]
        private BuffApplicationType applicationType;

        public abstract AttributeType BuffedAttributeType { get; }
        public abstract float GetValue(AttackContext context);
        public virtual float GetValueForText(AttackContext context) => GetValue(context);
    }
}