using Game.AttributeSystem.Buffs;
using Game.HealthSystem;

namespace Game.Weapons.Projectiles
{
    public class ProjectileOwnerInfo
    {
        public Health Health { get; }
        public BuffHolder WeaponBuffHolder { get; }

        public ProjectileOwnerInfo(Health health, BuffHolder weaponBuffHolder)
        {
            Health = health;
            WeaponBuffHolder = weaponBuffHolder;
        }
    }
}