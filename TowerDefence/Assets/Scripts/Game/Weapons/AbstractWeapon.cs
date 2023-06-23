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
        public WeaponType WeaponType => weaponType;
        [SerializeField]
        protected WeaponType weaponType;

        public Color Color => color;
        [SerializeField]
        private Color color;
        
        [SerializeField]
        protected AbstractTargetSelector targetSelectorPrefab;

        private AbstractTargetSelector targetSelector;
        
        protected Health ownerHealth;
        protected BuffHolder buffHolder;

        public AbstractAttributeOwner AttributeOwner => attributeOwner;
        protected AbstractAttributeOwner attributeOwner;
        
        public float GetAttackSpeed() => 1 / attributeOwner.GetValue(AttributeType.AttackInterval);

        public bool IsAttacking => isAttacking;
        private bool isAttacking;
        private float timeUntillNextAttack;

        protected Transform _transform;

        protected virtual void Awake()
        {
            ownerHealth = GetComponentInParent<Health>();
            buffHolder = GetComponent<BuffHolder>();
            attributeOwner = GetComponent<AbstractAttributeOwner>();
        }

        protected void Start()
        {
            Reset();
            _transform = transform;
            targetSelector = Instantiate(targetSelectorPrefab, _transform);
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
                var targets = targetSelector.SelectTargets(_transform.position, attackRange);
                if (targets != null && targets.Length > 0)
                {
                    isAttacking = true;
                    Attack(targets);
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

        protected virtual void Attack(TargetInfo target, TargetInfo[] allTargets) { }

        public void Reset()
        {
            timeUntillNextAttack = 0f;
            isAttacking = false;
        }
    }
}