using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class ClosestUnitFocusFireTargetSelector : ClosestUnitTargetSelector
    {
        private TargetInfo? currentTarget;

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

            var targets = base.SelectTargets(selectorPosition, attackRange);
            if (targets != null && targets.Length > 0)
            {
                currentTarget = targets[0];
            }

            return targets;
        }
    }
}