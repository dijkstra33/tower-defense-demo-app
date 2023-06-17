using Core.ObjectPooling;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public abstract class AbstractWeapon : MonoBehaviour, IResettable
    {
        [SerializeField]
        protected TargetSelector targetSelector;

        [SerializeField]
        private float attackInterval;

        [SerializeField]
        private float attackRange;
        
        [SerializeField]
        protected float attackDamage;

        public bool IsAttacking => isAttacking;
        private bool isAttacking;
        private float timeUntillNextAttack;

        protected Transform _transform;

        protected void Start()
        {
            Reset();
            _transform = transform;
        }

        protected void Update()
        {
            timeUntillNextAttack -= Time.deltaTime;
            if (timeUntillNextAttack <= 0)
            {
                var target = targetSelector.SelectTarget(_transform.position, attackRange);
                if (target != null)
                {
                    isAttacking = true;
                    Attack(target.Value);
                    timeUntillNextAttack = attackInterval;
                }
                else
                {
                    isAttacking = false;
                    // Ready to attack, just waiting for target.
                }
            }
        }

        protected abstract void Attack(TargetInfo target);

        public void Reset()
        {
            timeUntillNextAttack = attackInterval;
            isAttacking = false;
        }
    }
}