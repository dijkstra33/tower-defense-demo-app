using System.Collections.Generic;
using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class RandomTowerAttackerUnitTargetSelector : AbstractTargetSelector
    {
        private System.Random random = new();
        private BattleContext towerBattleContext;
        
        private void Start()
        {
            towerBattleContext = Tower.Instance.GetComponent<BattleContext>();
        }
        
        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            var allUnits = FindObjectsOfType<Unit>();
            var potentialTargets = new List<Unit>();

            foreach (var unit in allUnits)
            {
                var towerHasBeenHitByUnit = false;
                foreach (var unitWeapon in unit.Weapons)
                {
                    var unitWeaponBuffHolder = unitWeapon.GetComponent<BuffHolder>();
                    towerHasBeenHitByUnit |= towerBattleContext.GetHitsCountBy(unitWeaponBuffHolder) > 0;
                }

                var distance = Vector3.Distance(unit.Transform.position, selectorPosition);
                if (!towerHasBeenHitByUnit || !unit.gameObject.activeInHierarchy || distance > attackRange)
                {
                    continue;
                }

                potentialTargets.Add(unit);
            }

            if (potentialTargets.Count == 0)
            {
                return null;
            }

            var randomIndex = random.Next(0, potentialTargets.Count - 1);
            var targetUnit = potentialTargets[randomIndex];
            var targetInfo = new TargetInfo(targetUnit.GetComponent<Health>(), targetUnit.Transform);

            return new[] { targetInfo };
        }
    }
}