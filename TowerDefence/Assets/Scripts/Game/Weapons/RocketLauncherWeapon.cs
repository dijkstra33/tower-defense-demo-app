using Game.Weapons.Launchers;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public class RocketLauncherWeapon : AbstractWeapon
    {
        [SerializeField]
        private Launcher launcher;
        
        protected override void Attack(TargetInfo target)
        {
            launcher.Launch(_transform, target, attackDamage);
        }
    }
}