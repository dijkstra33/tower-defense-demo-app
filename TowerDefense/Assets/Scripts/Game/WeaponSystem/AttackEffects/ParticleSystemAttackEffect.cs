﻿using Game.AttributeSystem;
using Game.WeaponSystem.TargetSelectors;
using UnityEngine;

namespace Game.WeaponSystem.AttackEffects
{
    public class ParticleSystemAttackEffect : AbstractWeaponAttackEffect
    {
        [SerializeField]
        private ParticleSystem attackParticleSystem;

        private short particlesCount;
        
        protected override void Awake()
        {
            base.Awake();
            particlesCount = attackParticleSystem.emission.GetBurst(0).maxCount;
        }
        
        protected override void HandleWeaponAttack(TargetInfo[] targets, AbstractAttributeOwner weaponAttributeOwner)
        {
            var particleSystemMain = attackParticleSystem.main;
            particleSystemMain.startSpeed = weaponAttributeOwner.GetValue(AttributeType.AttackRange);
            attackParticleSystem.Emit(particlesCount);
            attackParticleSystem.Play();
        }
    }
}