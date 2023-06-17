using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons
{
    public abstract class AbstractWeapon : MonoBehaviour
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
            targetSelector.SetAttackRange(attackRange);
            timeUntillNextAttack = attackInterval;
            _transform = transform;
        }

        protected void Update()
        {
            timeUntillNextAttack -= Time.deltaTime;
            if (timeUntillNextAttack <= 0)
            {
                var target = targetSelector.SelectTarget(_transform.position);
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
    }
}