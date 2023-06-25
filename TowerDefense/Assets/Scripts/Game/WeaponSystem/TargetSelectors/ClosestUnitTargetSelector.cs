using System.Collections.Generic;
using UnityEngine;

namespace Game.WeaponSystem.TargetSelectors
{
    public class ClosestUnitTargetSelector : AbstractUnitTargetSelector
    {
        protected override TargetInfo[] FinalizeResult(List<Unit> filteredTargets, Vector3 selectorPosition, float selectRange)
        {
            var closestUnit = GetClosestUnit(filteredTargets, selectorPosition, selectRange);
            return ToTargetInfo(closestUnit);
        }

        protected Unit GetClosestUnit(List<Unit> filteredTargets, Vector3 selectorPosition, float selectRange)
        {
            var closestDistance = float.MaxValue;
            Unit closestTarget = null;
            foreach (var filteredTarget in filteredTargets)
            {
                var distance = Vector3.Distance(filteredTarget.Transform.position, selectorPosition);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = filteredTarget;
                }
            }

            return closestTarget;
        }
    }
}