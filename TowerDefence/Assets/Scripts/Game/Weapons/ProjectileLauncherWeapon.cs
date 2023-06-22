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
        
        protected override void Attack(TargetInfo target)
        {
            projectileLauncher.Launch(_transform.position, target, GetProjectileParams());
        }

        private ProjectileParams GetProjectileParams()
        {
            return new(
                damage: GetAttackDamage(), 
                moveSpeed: projectileMoveSpeed,
                minExplodeDistance: projectileMinExplodeDistance);
        }
    }
}