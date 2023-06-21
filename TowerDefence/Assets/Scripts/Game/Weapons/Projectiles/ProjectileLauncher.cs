using Core.ObjectPooling;
using Game.HealthSystem;
using Game.Spawning;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.Projectiles
{
    [CreateAssetMenu(menuName = "Game/Projectile Launcher")]
    public class ProjectileLauncher : ScriptableObject
    {
        [SerializeField]
        protected Projectile projectilePrefab;
        
        public void Launch(Health attackerHealth, Vector3 attackerPosition, TargetInfo target, float attackDamage)
        {
            var direction = (target.Transform.position - attackerPosition).normalized;
            var projectile = 
                ObjectPoolManager.Instance.GetObject(
                    projectilePrefab, attackerPosition, Quaternion.LookRotation(direction), 
                    projectilePrefab.transform.localScale, SpawnManager.ProjectilesRoot);

            projectile.Fire(attackerHealth, target, attackDamage);
        }
    }
}