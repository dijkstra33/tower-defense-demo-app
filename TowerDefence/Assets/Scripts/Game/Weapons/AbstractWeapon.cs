using System;
using Core.ObjectPooling;
using Game.AttributeSystem;
using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
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
        protected BuffHolder weaponBuffHolder;

        public AbstractAttributeOwner AttributeOwner => attributeOwner;
        protected AbstractAttributeOwner attributeOwner;
        
        public bool IsAttacking => isAttacking;
        private bool isAttacking;
        private float timeUntillNextAttack;

        protected Transform cachedTransform;

        protected virtual void Awake()
        {
            cachedTransform = transform;
            weaponOwnerHealth = GetComponentInParent<Health>();
            weaponBuffHolder = GetComponent<BuffHolder>();
            attributeOwner = GetComponent<AbstractAttributeOwner>();
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