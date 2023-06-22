using Game.AttributeSystem;
using Game.Weapons.Projectiles;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public class ProjectileLauncherWeapon : AbstractWeapon
    {
        [SerializeField] 
        private Projectile projectilePrefab;
        
        [SerializeField]
        private float projectileMoveSpeed;

        [SerializeField]
        private float projectileMinExplodeDistance;
        
        private ProjectileLauncher projectileLauncher;

        protected override void Awake()
        {
            base.Awake();
            projectileLauncher = new ProjectileLauncher(projectilePrefab, ownerHealth);
        }
        
        protected override void Attack(TargetInfo target, AttackContext attackContext)
        {
            projectileLauncher.Launch(_transform.position, target, GetProjectileParams(attackContext));
        }

        private ProjectileParams GetProjectileParams(AttackContext attackContext)
        {
            var attackDamage = attributeOwner.GetValue(AttributeType.Damage, attackContext);
            return new(
                damage: attackDamage, 
                moveSpeed: projectileMoveSpeed,
                minExplodeDistance: projectileMinExplodeDistance);
        }
    }
}