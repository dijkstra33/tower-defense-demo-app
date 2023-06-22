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
            return new(
                damage: GetAttackDamage(attackContext), 
                moveSpeed: projectileMoveSpeed,
                minExplodeDistance: projectileMinExplodeDistance);
        }
    }
}