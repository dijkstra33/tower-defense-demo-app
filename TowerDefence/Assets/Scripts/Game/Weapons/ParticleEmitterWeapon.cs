using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public class ParticleEmitterWeapon : AbstractWeapon
    {
        [SerializeField]
        private ParticleSystem attackParticleSystem;

        private short particlesCount;

        private void Awake()
        {
            particlesCount = attackParticleSystem.emission.GetBurst(0).maxCount;
        }

        protected override void Attack(TargetInfo[] targets)
        {
            var particleSystemMain = attackParticleSystem.main;
            particleSystemMain.startSpeed = GetAttackRange();
            attackParticleSystem.Emit(particlesCount);
            attackParticleSystem.Play();

            foreach (var target in targets)
            {
                target.Health.ReceiveDamage(GetAttackDamage());
            }
        }
    }
}