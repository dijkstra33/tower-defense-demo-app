using TowerDefence.Launchers;
using TowerDefence.TargetSelection;
using UnityEngine;

namespace TowerDefence.Weapons
{
    public class RocketLauncherWeapon : AbstractWeapon
    {
        [SerializeField]
        private Launcher launcher;
        
        protected override void Attack(TargetInfo target)
        {
            launcher.Launch(_transform.position, target, attackDamage);
        }
    }
}