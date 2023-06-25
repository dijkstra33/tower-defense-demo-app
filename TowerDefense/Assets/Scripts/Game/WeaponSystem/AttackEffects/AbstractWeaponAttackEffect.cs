using Game.AttributeSystem;
using Game.WeaponSystem.TargetSelectors;
using UnityEngine;

namespace Game.WeaponSystem.AttackEffects
{
    [RequireComponent(typeof(AbstractWeapon))]
    public abstract class AbstractWeaponAttackEffect : MonoBehaviour
    {
        protected virtual void Awake()
        {
            var weapon = GetComponent<AbstractWeapon>();
            weapon.OnAttack += (targets) => HandleWeaponAttack(targets, weapon.AttributeOwner);
        }

        protected abstract void HandleWeaponAttack(TargetInfo[] targets, AbstractAttributeOwner weaponAttributeOwner);
    }
}