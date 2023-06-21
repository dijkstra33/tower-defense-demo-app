using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class ClosestUnitFocusFireTargetSelector : ClosestUnitTargetSelector
    {
        private TargetInfo? target;

        private void Start()
        {
            DeathManager.Instance.OnUnitDeath += HandleUnitDied;
        }
        
        private void HandleUnitDied(Unit unit)
        {
            if (target.HasValue && target.Value.Transform == unit.transform)
            {
                target = null;
            }
        }

        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            if (target.HasValue && target.Value.Transform.gameObject.activeInHierarchy)
            {
                return new [] { target.Value };
            }

            var targets = base.SelectTargets(selectorPosition, attackRange);
            if (targets != null && targets.Length > 0)
            {
                target = targets[0];
            }

            return targets;
        }
    }
}