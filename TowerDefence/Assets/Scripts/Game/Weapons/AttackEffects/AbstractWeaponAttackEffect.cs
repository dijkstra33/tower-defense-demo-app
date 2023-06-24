using Game.AttributeSystem;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.AttackEffects
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