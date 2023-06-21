using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    [CreateAssetMenu(menuName = "Game/TargetSelectors/" + nameof(ClosestUnitTargetSelector))]
    public class ClosestUnitTargetSelector : TargetSelector
    {
        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            var units = FindObjectsOfType<Unit>();
            var closestDistance = float.MaxValue;
            Unit closestTarget = null;

            foreach (var unit in units)
            {
                var distance = Vector3.Distance(unit.Transform.position, selectorPosition);
                if (distance > attackRange)
                {
                    continue;
                }
                
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = unit;
                }
            }

            if (closestTarget == null)
            {
                return null;
            }

            return new []
            {
                new TargetInfo(closestTarget.GetComponent<Health>(), closestTarget.Transform),
            };
        }
    }
}