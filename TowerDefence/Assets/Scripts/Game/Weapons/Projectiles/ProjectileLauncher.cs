using Core.ObjectPooling;
using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using Game.SpawnSystem;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.Projectiles
{
    public class ProjectileLauncher
    {
        private readonly Projectile projectilePrefab;
        private readonly ProjectileOwnerInfo projectileOwnerInfo;

        public ProjectileLauncher(Projectile projectilePrefab, Health attackerHealth, BuffHolder weaponBuffHolder)
        {
            this.projectilePrefab = projectilePrefab;
            projectileOwnerInfo = new ProjectileOwnerInfo(attackerHealth, weaponBuffHolder);
        }
        
        public void Launch(Vector3 attackerPosition, TargetInfo target, ProjectileParams projectileParams)
        {
            var direction = (target.Transform.position - attackerPosition).normalized;
            var projectile = 
                ObjectPoolManager.Instance.GetObject(
                    projectilePrefab, attackerPosition, Quaternion.LookRotation(direction), 
                    projectilePrefab.transform.localScale, SpawnManager.ProjectilesRoot);

            projectile.Fire(projectileOwnerInfo, target, projectileParams);
        }
    }
}