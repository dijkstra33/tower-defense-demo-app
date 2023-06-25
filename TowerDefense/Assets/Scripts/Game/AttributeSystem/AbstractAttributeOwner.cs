using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using Game.WeaponSystem;
using UnityEngine;

namespace Game.AttributeSystem
{
    [RequireComponent(typeof(BuffOwner))]
    public abstract class AbstractAttributeOwner : MonoBehaviour
    {
        private Armor armor;
        protected BuffOwner BuffOwner;
        
        private void Awake()
        {
            armor = GetComponent<Armor>();
            BuffOwner = GetComponent<BuffOwner>(); 
        }

        public virtual float GetValue(AttributeType attributeType, AttackContext attackContext = null) 
            => BuffOwner.GetBuffedValue(0, attributeType, attackContext);

        protected int GetArmor()
        {
            var baseArmor = armor != null ? armor.BaseArmor : 0;
            return (int)BuffOwner.GetBuffedValue(baseArmor, AttributeType.Armor);
        }
    }
}