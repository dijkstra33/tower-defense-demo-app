using System.Collections.Generic;
using Game.HealthSystem;
using UnityEngine;

namespace Game.WeaponSystem.TargetSelectors
{
    public class ClosestUnitFocusFireTargetSelector : ClosestUnitTargetSelector
    {
        private Unit currentTarget;

        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDied;
        }
        
        private void HandleUnitDied(Unit unit, AbstractWeapon killerWeapon, Health killerHealth)
        {
            if (currentTarget != null && currentTarget.Transform == unit.transform)
            {
                currentTarget = null;
            }
        }

        protected override TargetInfo[] FinalizeResult(List<Unit> filteredTargets, Vector3 selectorPosition, float selectRange)
        {
            if (currentTarget != null)
            {
                return ToTargetInfo(currentTarget);
            }

            currentTarget = GetClosestUnit(filteredTargets, selectorPosition, selectRange);
            return ToTargetInfo(currentTarget);
        }
    }
}