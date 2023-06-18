using System;
using Core.ObjectPooling;
using UnityEngine;

namespace Game.HealthSystem
{
    public class Health : MonoBehaviour, IResettable
    {
        [SerializeField]
        private int maxValue;
        
        private int currentValue;
        private bool isDead = false;

        private void Start()
        {
            currentValue = maxValue;
        }

        public void DealDamage(float damage)
        {
            if (isDead)
            {
                return;
            }
            
            currentValue = Math.Max(currentValue - (int)damage, 0);

            if (currentValue == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            var deathHandlers = GetComponents<IDeathHandler>();
            foreach (var deathHandler in deathHandlers)
            {
                deathHandler.OnDeath();
            }
            DeathManager.Instance.OnDeath(gameObject);
        }

        public void Reset()
        {
            isDead = false;
            currentValue = maxValue;
        }
    }
}
