using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.Projectiles
{
    // TODO: add on fire particle effects controlled by separate component.
    public class Projectile : MonoBehaviour
    {
        private bool isFired = false;

        private Health.Health targetHealth;
        private Transform targetTransform;
        private float damage;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float minExplodeDistance;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void Fire(TargetInfo target, float damage)
        {
            if (isFired)
            {
                return;
            }

            targetHealth = target.Health;
            targetTransform = target.Transform;
            this.damage = damage;
            isFired = true;
        }

        private void Update()
        {
            if (!isFired)
            {
                return;
            }

            var distanceToTarget = Vector3.Distance(_transform.position, targetTransform.position);
            if (distanceToTarget < minExplodeDistance)
            {
                Explode();
            }
            else
            {
                Move();
            }
        }

        private void Move()
        {
            var direction = (targetTransform.position - _transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        private void Explode()
        {
            targetHealth.DealDamage(damage);
            Destroy(gameObject);
        }
    }
}