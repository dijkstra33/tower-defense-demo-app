using System.Collections.Generic;
using Game.HealthSystem;
using UnityEngine;

namespace Game.WeaponSystem.TargetSelectors
{
    public abstract class AbstractUnitTargetSelector : AbstractTemplatedTargetSelector<Unit>
    {
        protected override Unit[] GetPotentialTargets()
        {
            return FindObjectsOfType<Unit>();
        }
        
        protected override bool MatchObligatoryFilter(Unit potentialTarget, Vector3 selectorPosition, float selectRange)
        {
            var distance = Vector3.Distance(potentialTarget.Transform.position, selectorPosition);
            return potentialTarget.gameObject.activeInHierarchy && distance <= selectRange;
        }

        protected override TargetInfo[] FinalizeResult(List<Unit> filteredTargets, Vector3 selectorPosition, float selectRange)
        {
            return ToTargetInfo(filteredTargets);
        }

        protected TargetInfo[] ToTargetInfo(List<Unit> units)
        {
            var targetInfoArray = new TargetInfo[units.Count];
            for (var i = 0; i < units.Count; i++)
            {
                var unit = units[i];
                targetInfoArray[i] = new TargetInfo(unit.GetComponent<Health>(), unit.Transform);
            }

            return targetInfoArray;
        }

        protected TargetInfo[] ToTargetInfo(Unit unit)
        {
            if (unit == null)
            {
                return null;
            }
            
            return new [] { new TargetInfo(unit.GetComponent<Health>(), unit.Transform) };
        }
    }
}