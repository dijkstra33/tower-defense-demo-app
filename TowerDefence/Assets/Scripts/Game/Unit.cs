using Core.ObjectPooling;
using Game.AttributeSystem;
using Game.HealthSystem;
using Game.Weapons;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Health), typeof(AbstractAttributeOwner), typeof(BattleContext))]
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

        public AbstractWeapon[] Weapons => weapons;
        private AbstractWeapon[] weapons;
        
        private UnitState state = UnitState.Idle;

        public Transform Transform => cachedTransform;
        private Transform cachedTransform;

        private Transform targetTransform;

        public AbstractAttributeOwner AttributeOwner => attributeOwner;
        private AbstractAttributeOwner attributeOwner;

        private void Awake()
        {
            attributeOwner = GetComponent<AbstractAttributeOwner>();
        }

        private void Start()
        {
            cachedTransform = transform;
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
            var direction = (targetTransform.position - cachedTransform.position).normalized;
            transform.position += direction * (moveSpeed * Time.deltaTime);
        }

        public void Reset()
        {
            state = UnitState.Idle;
        }
    }
}