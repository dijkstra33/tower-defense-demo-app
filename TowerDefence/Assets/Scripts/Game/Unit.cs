using Game.Weapons;
using UnityEngine;

namespace Game
{
    public class Unit : MonoBehaviour
    {
        private enum UnitState
        {
            Moving,
            Attacking,
        }
        
        [SerializeField]
        private float moveSpeed;

        private AbstractWeapon[] weapons;
        
        private UnitState state = UnitState.Moving;
        
        private Transform _transform;
        private Transform targetTransform;

        private void Start()
        {
            _transform = transform;
            weapons = GetComponentsInChildren<AbstractWeapon>();
        }

        public void SetData(Transform targetTransform)
        {
            this.targetTransform = targetTransform;
        }

        private void Update()
        {
            switch (state)
            {
                case UnitState.Moving:
                    Move();
                    break;
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
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}