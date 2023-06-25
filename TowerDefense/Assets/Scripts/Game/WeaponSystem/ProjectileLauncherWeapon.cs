using Game.AttributeSystem;
using Game.WeaponSystem.Projectiles;
using Game.WeaponSystem.TargetSelectors;
using UnityEngine;

namespace Game.WeaponSystem
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
            projectileLauncher = new ProjectileLauncher(projectilePrefab, weaponOwnerHealth, this);
        }
        
        protected override void Attack(TargetInfo target, TargetInfo[] allTargets)
        {
            var attackContext = new AttackContext(target, allTargets, this);
            projectileLauncher.Launch(cachedTransform.position, target, GetProjectileParams(attackContext));
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