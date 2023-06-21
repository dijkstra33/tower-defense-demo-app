using Game.Weapons.Projectiles;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public class ProjectileLauncherWeapon : AbstractWeapon
    {
        [SerializeField]
        private ProjectileLauncher projectileLauncher;
        
        protected override void Attack(TargetInfo target)
        {
            projectileLauncher.Launch(_transform.position, target, GetAttackDamage());
        }
    }
}