using System;
using Core.ObjectPooling;
using UnityEngine;

namespace Game.HealthSystem
{
    public class Health : MonoBehaviour, IResettable
    {
        public event Action<Health> OnDamageReceived;
        public event Action OnDeath;
        
        public int MaxValue => maxValue;
        [SerializeField]
        private int maxValue;

        public int CurrentValue => currentValue;
        private int currentValue;

        private bool isDead = false;

        private Armored armored;

        private void Awake()
        {
            currentValue = maxValue;
            armored = GetComponent<Armored>();
        }

        public void ReceiveDamage(float damage, Health attackerHealth = null)
        {
            if (isDead)
            {
                return;
            }
            
            var pureDamage = armored != null ? armored.GetPureDamage(damage) : (int)damage;
            OnDamageReceived?.Invoke(attackerHealth);
            currentValue = Math.Max(currentValue - pureDamage, 0);

            if (currentValue == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            DeathManager.Instance.OnDeath(gameObject);
            OnDeath?.Invoke();
        }

        public void Reset()
        {
            isDead = false;
            currentValue = maxValue;
        }
    }
}
