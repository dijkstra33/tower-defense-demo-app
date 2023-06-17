using System;
using UnityEngine;

namespace Game.Health
{
    public class Health : MonoBehaviour
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
                isDead = true;
                var deathHandlers = GetComponents<IDeathHandler>();
                foreach (var deathHandler in deathHandlers)
                {
                    deathHandler.OnDeath();
                }
            }
        }
    }
}
