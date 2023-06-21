using System.Collections.Generic;
using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class RandomUnitFocusFireTargetSelector : TargetSelector
    {
        private TargetInfo? currentTarget;
        
        private System.Random random = new();

        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDied;
        }
        
        private void HandleUnitDied(Unit unit)
        {
            if (currentTarget.HasValue && currentTarget.Value.Transform == unit.transform)
            {
                currentTarget = null;
            }
        }

        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            if (currentTarget.HasValue && currentTarget.Value.Transform.gameObject.activeInHierarchy)
            {
                return new [] { currentTarget.Value };
            }

            var units = FindObjectsOfType<Unit>();
            var potentialTargets = new List<Unit>();
            
            foreach (var unit in units)
            {
                var distance = Vector3.Distance(unit.Transform.position, selectorPosition);
                if (!unit.gameObject.activeInHierarchy || distance > attackRange)
                {
                    continue;
                }

                potentialTargets.Add(unit);
            }
            
            if (potentialTargets.Count == 0)
            {
                return null;
            }

            var randomIndex = random.Next(0, potentialTargets.Count - 1);
            var targetUnit = potentialTargets[randomIndex];
            var targetInfo = new TargetInfo(targetUnit.GetComponent<Health>(), targetUnit.Transform);
            currentTarget = targetInfo;

            return new[] { targetInfo };
        }
    }
}