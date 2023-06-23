using Game.AttributeSystem.Buffs;
using Game.HealthSystem;

namespace Game.Weapons.Projectiles
{
    public class ProjectileOwnerInfo
    {
        public Health Health { get; }
        public BuffHolder BuffHolder { get; }

        public ProjectileOwnerInfo(Health health, BuffHolder buffHolder)
        {
            Health = health;
            BuffHolder = buffHolder;
        }
    }
}