using Game.AttributeSystem;
using Game.WeaponSystem.TargetSelectors;

namespace Game.WeaponSystem
{
    public class InstantDamageWeapon : AbstractWeapon
    {
        protected override void Attack(TargetInfo target, TargetInfo[] allTargets)
        {
            var attackContext = new AttackContext(target, allTargets, this);
            var attackDamage = attributeOwner.GetValue(AttributeType.Damage, attackContext);
            target.Health.ReceiveDamage(attackDamage, this, weaponOwnerHealth);
        }
    }
}