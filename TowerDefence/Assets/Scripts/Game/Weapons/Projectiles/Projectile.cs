using Core.ObjectPooling;
using Game.HealthSystem;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.Projectiles
{
    // TODO: add on fire particle effects controlled by separate component.
    public class Projectile : MonoBehaviour, IResettable
    {
        private bool isFired = false;

        private Health targetHealth;
        private Transform targetTransform;
        private float damage;

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float minExplodeDistance;

        private Health attackerHealth;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void Fire(Health attackerHealth, TargetInfo target, float damage)
        {
            if (isFired)
            {
                return;
            }

            this.attackerHealth = attackerHealth;
            targetHealth = target.Health;
            targetTransform = target.Transform;
            this.damage = damage;
            isFired = true;

            attackerHealth.OnDeath += HandleOnAttackerDeath;
        }

        private void HandleOnAttackerDeath()
        {
            attackerHealth.OnDeath -= HandleOnAttackerDeath;
            attackerHealth = null;
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
            targetHealth.ReceiveDamage(damage, attackerHealth);
            DeathManager.Instance.OnDeath(gameObject);
        }

        public void Reset()
        {
            isFired = false;
            if (attackerHealth != null)
            {
                attackerHealth.OnDeath -= HandleOnAttackerDeath;
                attackerHealth = null;
            }
        }
    }
}