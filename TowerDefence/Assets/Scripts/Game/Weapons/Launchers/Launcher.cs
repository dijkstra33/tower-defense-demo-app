using Core.ObjectPooling;
using Game.Spawning;
using Game.Weapons.Projectiles;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.Launchers
{
    [CreateAssetMenu(menuName = "Game/Launcher")]
    public class Launcher : ScriptableObject
    {
        [SerializeField]
        protected Projectile projectilePrefab;
        
        public void Launch(Vector3 attackerPosition, TargetInfo target, float attackDamage)
        {
            var direction = (target.Transform.position - attackerPosition).normalized;
            var projectile = 
                ObjectPoolManager.Instance.GetObject(
                    projectilePrefab, attackerPosition, Quaternion.LookRotation(direction), 
                    projectilePrefab.transform.localScale, SpawnManager.ProjectilesRoot);

            projectile.Fire(target, attackDamage);
        }
    }
}