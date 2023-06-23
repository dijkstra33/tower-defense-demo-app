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
        
        private ProjectileOwnerInfo projectileOwnerInfo;
        private TargetInfo targetInfo;
        private ProjectileParams projectileParams;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void Fire(ProjectileOwnerInfo projectileOwnerInfo, TargetInfo targetInfo, ProjectileParams projectileParams)
        {
            if (isFired)
            {
                return;
            }

            this.projectileOwnerInfo = projectileOwnerInfo;
            this.projectileOwnerInfo.Health.OnDeath += HandleOnAttackerDeath;
            
            this.targetInfo = targetInfo;
            this.projectileParams = projectileParams;
            isFired = true;
        }

        private void HandleOnAttackerDeath()
        {
            projectileOwnerInfo.Health.OnDeath -= HandleOnAttackerDeath;
            projectileOwnerInfo = null;
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

            targetInfo.Health.ReceiveDamage(projectileParams.Damage, projectileOwnerInfo?.WeaponBuffHolder, projectileOwnerInfo?.Health);
            DeathManager.Instance.OnDeath(gameObject, null, null);
            
            if (projectileOwnerInfo != null)
            {
                projectileOwnerInfo.Health.OnDeath -= HandleOnAttackerDeath;
                projectileOwnerInfo = null;
            }
        }
    }
}