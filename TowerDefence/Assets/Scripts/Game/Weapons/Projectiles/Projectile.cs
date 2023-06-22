using Core.ObjectPooling;
using Game.HealthSystem;
using Game.Weapons.TargetSelection;
using UnityEngine;

namespace Game.Weapons.Projectiles
{
    // TODO: add on fire particle effects controlled by separate component.
    [RequireComponent(typeof(Poolable))]
    public class Projectile : MonoBehaviour
    {
        private bool isFired = false;
        
        private Health attackerHealth;
        private TargetInfo targetInfo;
        private ProjectileParams projectileParams;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void Fire(Health attackerHealth, TargetInfo targetInfo, ProjectileParams projectileParams)
        {
            if (isFired)
            {
                return;
            }

            this.attackerHealth = attackerHealth;
            this.targetInfo = targetInfo;
            this.projectileParams = projectileParams;
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

            var distanceToTarget = Vector3.Distance(_transform.position, targetInfo.Transform.position);
            if (distanceToTarget < projectileParams.MinExplodeDistance)
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
            var direction = (targetInfo.Transform.position - _transform.position).normalized;
            transform.position += direction * projectileParams.MoveSpeed * Time.deltaTime;
        }

        private void Explode()
        {
            isFired = false;
            
            targetInfo.Health.ReceiveDamage(projectileParams.Damage, attackerHealth);
            DeathManager.Instance.OnDeath(gameObject);
            
            if (attackerHealth != null)
            {
                attackerHealth.OnDeath -= HandleOnAttackerDeath;
                attackerHealth = null;
            }
        }
    }
}