using Core.ObjectPooling;
using Game.HealthSystem;
using Game.Weapons;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Health))]
    public class Unit : MonoBehaviour, IResettable
    {
        private enum UnitState
        {
            Idle,
            Moving,
            Attacking,
        }
        
        [SerializeField]
        private float moveSpeed;

        public int DeathCurrencyReward => deathCurrencyReward;
        [SerializeField]
        private int deathCurrencyReward;

        private AbstractWeapon[] weapons;
        
        private UnitState state = UnitState.Idle;

        public Transform Transform => _transform;
        private Transform _transform;

        private Transform targetTransform;

        private void Start()
        {
            _transform = transform;
            weapons = GetComponentsInChildren<AbstractWeapon>();
        }

        public void MoveTo(Transform targetTransform)
        {
            this.targetTransform = targetTransform;
            state = UnitState.Moving;
        }

        private void Update()
        {
            if (GameManager.Instance.GameOver)
            {
                return;
            }
            
            switch (state)
            {
                case UnitState.Moving:
                    Move();
                    break;
                case UnitState.Idle:
                case UnitState.Attacking:
                    // TODO: do we really need this states here? :|
                    break;
            }

            TryToChangeState();
        }

        private void TryToChangeState()
        {
            if (state == UnitState.Moving)
            {
                var allWeaponsAttacking = true;
                foreach (var weapon in weapons)
                {
                    if (!weapon.IsAttacking)
                    {
                        allWeaponsAttacking = false;
                        break;
                    }
                }

                if (allWeaponsAttacking)
                {
                    state = UnitState.Attacking;
                }
            }
        }

        private void Move()
        {
            var direction = (targetTransform.position - _transform.position).normalized;
            transform.position += direction * (moveSpeed * Time.deltaTime);
        }

        public void Reset()
        {
            state = UnitState.Idle;
        }
    }
}