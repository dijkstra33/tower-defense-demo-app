using System;
using Core.ObjectPooling;
using UnityEngine;

namespace Game.HealthSystem
{
    public class Health : MonoBehaviour, IResettable
    {
        public int MaxValue => maxValue;
        [SerializeField]
        private int maxValue;

        public int CurrentValue => currentValue;
        private int currentValue;

        private bool isDead = false;

        private void Awake()
        {
            currentValue = maxValue;
        }

        public void ReceiveDamage(float damage)
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
            DeathManager.Instance.OnDeath(gameObject);
        }

        public void Reset()
        {
            isDead = false;
            currentValue = maxValue;
        }
    }
}
