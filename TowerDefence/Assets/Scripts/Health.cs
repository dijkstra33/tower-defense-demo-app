using System;
using UnityEngine;

namespace TowerDefence
{
    public class Health : MonoBehaviour
    {
        private int currentValue;

        [SerializeField]
        private int maxValue;

        private void Start()
        {
            currentValue = maxValue;
        }

        public void DealDamage(float damage)
        {
            currentValue = Math.Max(currentValue - (int)damage, 0);
        }
    }
}
