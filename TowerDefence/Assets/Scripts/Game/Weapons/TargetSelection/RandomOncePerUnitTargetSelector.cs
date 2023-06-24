using System.Collections.Generic;
using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class RandomOncePerUnitTargetSelector : RandomUnitTargetSelector
    {
        private readonly HashSet<Unit> targetedUnits = new();
        
        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDied;
        }
        
        private void HandleUnitDied(Unit unit, BuffHolder weaponBuffHolder, Health killerHealth)
        {
            targetedUnits.Remove(unit);
        }

        protected override bool MatchFilter(Unit potentialTarget, Vector3 selectorPosition, float selectRange)
        {
            return base.MatchFilter(potentialTarget, selectorPosition, selectRange) 
                   && !targetedUnits.Contains(potentialTarget);
        }

        protected override TargetInfo[] FinalizeResult(List<Unit> filteredTargets, Vector3 selectorPosition, float selectRange)
        {
            var randomUnit = GetRandomUnit(filteredTargets);
            if (randomUnit != null)
            {
                targetedUnits.Add(randomUnit);
            }

            return ToTargetInfo(randomUnit);
        }
    }
}