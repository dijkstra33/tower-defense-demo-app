using Game.HealthSystem;

namespace Game.Weapons.Projectiles
{
    public class ProjectileOwnerInfo
    {
        public Health Health { get; }
        public AbstractWeapon Weapon { get; }

        public ProjectileOwnerInfo(Health health, AbstractWeapon weapon)
        {
            Health = health;
            Weapon = weapon;
        }
    }
}