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
            // TODO: use some sort of object pool.
            var projectile = Instantiate(projectilePrefab, attackerPosition, Quaternion.LookRotation(direction));
            projectile.Fire(target, attackDamage);
        }
    }
}