using Core.ObjectPooling;
using Game.HealthSystem;
using Game.Spawning;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.Projectiles
{
    public class ProjectileLauncher
    {
        private readonly Projectile projectilePrefab;
        private readonly Health attackerHealth;

        public ProjectileLauncher(Projectile projectilePrefab, Health attackerHealth)
        {
            this.projectilePrefab = projectilePrefab;
            this.attackerHealth = attackerHealth;
        }
        
        public void Launch(Vector3 attackerPosition, TargetInfo target, ProjectileParams projectileParams)
        {
            var direction = (target.Transform.position - attackerPosition).normalized;
            var projectile = 
                ObjectPoolManager.Instance.GetObject(
                    projectilePrefab, attackerPosition, Quaternion.LookRotation(direction), 
                    projectilePrefab.transform.localScale, SpawnManager.ProjectilesRoot);

            projectile.Fire(attackerHealth, target, projectileParams);
        }
    }
}