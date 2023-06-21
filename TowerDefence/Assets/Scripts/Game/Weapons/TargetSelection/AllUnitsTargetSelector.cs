﻿using System.Collections.Generic;
using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    [CreateAssetMenu(menuName = "Game/TargetSelectors/" + nameof(AllUnitsTargetSelector))]
    public class AllUnitsTargetSelector : TargetSelector
    {
        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            var units = FindObjectsOfType<Unit>();
            var targets = new List<TargetInfo>();
            
            foreach (var unit in units)
            {
                var distance = Vector3.Distance(unit.Transform.position, selectorPosition);
                if (distance > attackRange)
                {
                    continue;
                }

                var targetInfo = new TargetInfo(unit.GetComponent<Health>(), unit.Transform);
                targets.Add(targetInfo);
            }

            return targets.ToArray();
        }
    }
}