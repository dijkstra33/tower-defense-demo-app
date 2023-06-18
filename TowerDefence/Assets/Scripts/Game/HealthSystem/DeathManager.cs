using System;
using UnityEngine;

namespace Game.HealthSystem
{
    public class DeathManager : MonoBehaviour
    {
        public event Action<Unit> OnUnitDeath;
        
        public static DeathManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void OnDeath(GameObject died)
        {
            var unit = died.GetComponent<Unit>();
            if (unit != null)
            {
                OnUnitDeath?.Invoke(unit);
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}