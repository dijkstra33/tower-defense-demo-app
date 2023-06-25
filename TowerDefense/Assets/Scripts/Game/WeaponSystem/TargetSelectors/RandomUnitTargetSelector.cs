using System.Collections.Generic;
using UnityEngine;

namespace Game.WeaponSystem.TargetSelectors
{
    public class RandomUnitTargetSelector : AbstractUnitTargetSelector
    {
        private static readonly System.Random random = new();
        
        protected override TargetInfo[] FinalizeResult(List<Unit> filteredTargets, Vector3 selectorPosition, float selectRange)
        {
            var randomUnit = GetRandomUnit(filteredTargets);
            return ToTargetInfo(randomUnit);
        }

        protected Unit GetRandomUnit(List<Unit> units)
        {
            if (units == null || units.Count == 0)
            {
                return null;
            }

            var randomIndex = random.Next(0, units.Count - 1);
            var randomUnit = units[randomIndex];
            return randomUnit;
        }
    }
}