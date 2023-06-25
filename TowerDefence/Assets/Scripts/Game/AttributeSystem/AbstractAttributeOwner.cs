using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem
{
    [RequireComponent(typeof(BuffHolder))]
    public abstract class AbstractAttributeOwner : MonoBehaviour
    {
        private Armor armor;
        protected BuffHolder buffHolder;
        
        private void Awake()
        {
            armor = GetComponent<Armor>();
            buffHolder = GetComponent<BuffHolder>(); 
        }

        public virtual float GetValue(AttributeType attributeType, AttackContext attackContext = null) 
            => buffHolder.GetBuffedValue(0, attributeType, attackContext);

        protected int GetArmor()
        {
            var baseArmor = armor != null ? armor.BaseArmor : 0;
            return (int)buffHolder.GetBuffedValue(baseArmor, AttributeType.Armor);
        }
    }
}