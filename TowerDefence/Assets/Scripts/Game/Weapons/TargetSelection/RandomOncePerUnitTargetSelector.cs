using System.Collections.Generic;
using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class RandomOncePerUnitTargetSelector : AbstractTargetSelector
    {
        private HashSet<Unit> targetedUnits = new();
        
        // TODO: put all random classes into one or use unity random instead?
        private System.Random random = new();

        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDied;
        }
        
        private void HandleUnitDied(Unit unit, BuffHolder weaponBuffHolder, Health killerHealth)
        {
            targetedUnits.Remove(unit);
        }
        
        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            var units = FindObjectsOfType<Unit>();
            var potentialTargets = new List<Unit>();
            
            foreach (var unit in units)
            {
                var distance = Vector3.Distance(unit.Transform.position, selectorPosition);
                var targetedBefore = targetedUnits.Contains(unit);
                if (!unit.gameObject.activeInHierarchy || distance > attackRange || targetedBefore)
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
            targetedUnits.Add(targetUnit);

            return new[] { targetInfo };
        }
    }
}