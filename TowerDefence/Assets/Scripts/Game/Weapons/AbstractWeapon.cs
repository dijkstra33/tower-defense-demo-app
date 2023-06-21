using Core.ObjectPooling;
using Game.HealthSystem;
using Game.UpgradeSystem;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public abstract class AbstractWeapon : MonoBehaviour, IResettable
    {
        public WeaponType WeaponType => weaponType;
        [SerializeField]
        protected WeaponType weaponType;

        public bool IsUpgradable => isUpgradable;
        [SerializeField]
        private bool isUpgradable;

        public Color Color => color;
        [SerializeField]
        private Color color;
        
        [SerializeField]
        protected TargetSelector targetSelectorPrefab;

        private TargetSelector targetSelector;
        protected Health ownerHealth;

        public float GetAttackSpeed() => 1 / attackInterval;
        [SerializeField]
        private float attackInterval;

        public float GetAttackRange() => attackRange;
        [SerializeField]
        private float attackRange;

        public float GetAttackDamage() => 
            attackDamage + UpgradeManager.Instance.GetUpgradeValue(UpgradeType.Damage, this);

        [SerializeField]
        private float attackDamage;

        public bool IsAttacking => isAttacking;
        private bool isAttacking;
        private float timeUntillNextAttack;

        protected Transform _transform;

        protected void Start()
        {
            Reset();
            _transform = transform;
            targetSelector = Instantiate(targetSelectorPrefab, _transform);
            ownerHealth = GetComponentInParent<Health>();
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
                var targets = targetSelector.SelectTargets(_transform.position, attackRange);
                if (targets != null && targets.Length > 0)
                {
                    isAttacking = true;
                    Attack(targets);
                    timeUntillNextAttack = attackInterval;
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
                Attack(target);
            }
        }

        protected virtual void Attack(TargetInfo target) { }

        public void Reset()
        {
            timeUntillNextAttack = attackInterval;
            isAttacking = false;
        }
    }
}