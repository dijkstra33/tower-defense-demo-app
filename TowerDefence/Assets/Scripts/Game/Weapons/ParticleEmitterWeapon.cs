using Game.AttributeSystem;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public class ParticleEmitterWeapon : AbstractWeapon
    {
        [SerializeField]
        private ParticleSystem attackParticleSystem;

        private short particlesCount;

        protected override void Awake()
        {
            base.Awake();
            particlesCount = attackParticleSystem.emission.GetBurst(0).maxCount;
        }

        protected override void Attack(TargetInfo[] targets)
        {
            var particleSystemMain = attackParticleSystem.main;
            particleSystemMain.startSpeed = attributeOwner.GetValue(AttributeType.AttackRange);
            attackParticleSystem.Emit(particlesCount);
            attackParticleSystem.Play();

            foreach (var target in targets)
            {
                var attackContext = new AttackContext(target, targets, weaponBuffHolder);
                var attackDamage = attributeOwner.GetValue(AttributeType.Damage, attackContext);
                target.Health.ReceiveDamage(attackDamage, weaponBuffHolder, weaponOwnerHealth);
            }
        }
    }
}