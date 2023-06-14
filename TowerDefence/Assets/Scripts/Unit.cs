using UnityEngine;

namespace TowerDefence
{
    public class Unit : MonoBehaviour
    {
        private enum UnitState
        {
            Moving,
        }
        
        [SerializeField]
        private float moveSpeed;
        
        private UnitState state = UnitState.Moving;
        
        private Transform _transform;
        private Transform targetTransform;

        private void Start()
        {
            _transform = transform;
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
            }
        }

        private void Move()
        {
            var direction = (targetTransform.position - _transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}