using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class ClosestUnitFocusFireTargetSelector : ClosestUnitTargetSelector
    {
        private TargetInfo? lastAttackedTarget;

        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDied;
        }
        
        private void HandleUnitDied(Unit unit)
        {
            if (lastAttackedTarget.HasValue && lastAttackedTarget.Value.Transform == unit.transform)
            {
                lastAttackedTarget = null;
            }
        }

        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            if (lastAttackedTarget.HasValue && lastAttackedTarget.Value.Transform.gameObject.activeInHierarchy)
            {
                return new [] { lastAttackedTarget.Value };
            }

            var targets = base.SelectTargets(selectorPosition, attackRange);
            if (targets != null && targets.Length > 0)
            {
                lastAttackedTarget = targets[0];
            }

            return targets;
        }
    }
}