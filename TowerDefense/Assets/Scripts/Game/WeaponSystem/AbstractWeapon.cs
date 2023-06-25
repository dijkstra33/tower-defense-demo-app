using System;
using System.Collections.Generic;
using Core.ObjectPooling;
using Game.AttributeSystem;
using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using Game.WeaponSystem.TargetSelectors;
using UnityEngine;

namespace Game.WeaponSystem
{
    [RequireComponent(typeof(WeaponAttributeOwner), typeof(BattleContext))]
    public abstract class AbstractWeapon : MonoBehaviour, IResettable
    {
        public event Action<TargetInfo[]> OnAttack;
        
        public WeaponVisualData VisualData => visualData;
        [SerializeField]
        private WeaponVisualData visualData;
        
        [SerializeField]
        protected AbstractTargetSelector targetSelectorPrefab;

        private AbstractTargetSelector targetSelector;
        
        protected Health weaponOwnerHealth;

        public List<AbstractBuff> GetBuffsToTargetOnHit() => weaponBuffOwner.BuffsToTargetOnHit;
        protected BuffOwner weaponBuffOwner;

        public WeaponAttributeOwner AttributeOwner => attributeOwner;
        protected WeaponAttributeOwner attributeOwner;
        
        public bool IsAttacking => isAttacking;
        private bool isAttacking;
        private float timeUntillNextAttack;

        protected Transform cachedTransform;

        protected virtual void Awake()
        {
            cachedTransform = transform;
            weaponOwnerHealth = GetComponentInParent<Health>();
            weaponBuffOwner = GetComponent<BuffOwner>();
            attributeOwner = GetComponent<WeaponAttributeOwner>();
        }

        protected void Start()
        {
            Reset();
            targetSelector = Instantiate(targetSelectorPrefab, cachedTransform);
            var towerHealth = Tower.Instance.GetComponent<Health>();
            towerHealth.OnDamageReceived += HandleDamageReceivedByTower;
        }

        private void HandleDamageReceivedByTower()
        {
            timeUntillNextAttack -= attributeOwner.GetValue(AttributeType.DecreaseAttackIntervalOnTowerHit);
        }

        protected virtual void Update()
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }
            
            timeUntillNextAttack -= Time.deltaTime;
            if (timeUntillNextAttack <= 0)
            {
                var attackRange = attributeOwner.GetValue(AttributeType.AttackRange);
                var targets = targetSelector.SelectTargets(cachedTransform.position, attackRange);
                if (targets != null && targets.Length > 0)
                {
                    isAttacking = true;
                    OnAttack?.Invoke(targets);
                    Attack(targets);
                    HealManager.Instance.TryHealAttacker(weaponOwnerHealth, attributeOwner);
                    timeUntillNextAttack = attributeOwner.GetValue(AttributeType.AttackInterval);
                }
                else
                {
                    isAttacking = false;
                    // Ready to attack, just waiting for target.
                }
            }
        }

        protected virtual void Attack(TargetInfo[] targets)
        {
            foreach (var target in targets)
            {
                Attack(target, targets);
            }
        }

        protected abstract void Attack(TargetInfo target, TargetInfo[] allTargets);

        public void Reset()
        {
            timeUntillNextAttack = 0f;
            isAttacking = false;
        }
    }
}