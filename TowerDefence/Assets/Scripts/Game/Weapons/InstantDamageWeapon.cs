using Game.AttributeSystem;
using Game.Weapons.TargetSelection;

namespace Game.Weapons
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