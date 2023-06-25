using System.Collections.Generic;
using Game.HealthSystem;
using UnityEngine;

namespace Game.WeaponSystem.TargetSelectors
{
    public class TowerTargetSelector : AbstractTemplatedTargetSelector<Tower>
    {
        protected override Tower[] GetPotentialTargets()
        {
            return new[] { Tower.Instance };
        }

        protected override bool MatchObligatoryFilter(Tower potentialTarget, Vector3 selectorPosition, float selectRange)
        {
            var distanceToTarget = Vector3.Distance(potentialTarget.gameObject.transform.position, selectorPosition);
            return distanceToTarget <= selectRange;
        }

        protected override TargetInfo[] FinalizeResult(List<Tower> filteredTargets, Vector3 selectorPosition, float selectRange)
        {
            if (filteredTargets.Count > 0)
            {
                var tower = filteredTargets[0];
                var towerHealth = tower.GetComponent<Health>();
                return new []
                {
                    new TargetInfo(towerHealth, tower.gameObject.transform),
                };
            }

            return null;
        }
    }
}