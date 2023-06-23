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

        protected override void Attack(TargetInfo[] targets, AttackContext attackContext)
        {
            var particleSystemMain = attackParticleSystem.main;
            particleSystemMain.startSpeed = attributeOwner.GetValue(AttributeType.AttackRange, attackContext);
            attackParticleSystem.Emit(particlesCount);
            attackParticleSystem.Play();

            var attackDamage = attributeOwner.GetValue(AttributeType.Damage, attackContext);
            foreach (var target in targets)
            {
                target.Health.ReceiveDamage(attackDamage, ownerHealth, buffHolder);
            }
        }
    }
}