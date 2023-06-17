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
        
        public void Launch(Transform attackerTransform, TargetInfo target, float attackDamage)
        {
            var attackerPosition = attackerTransform.position;
            var direction = (target.Transform.position - attackerPosition).normalized;
            // TODO: use some sort of object pool.
            var projectile = Instantiate(projectilePrefab, attackerPosition, Quaternion.LookRotation(direction), attackerTransform);
            projectile.Fire(target, attackDamage);
        }
    }
}