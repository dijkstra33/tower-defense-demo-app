using System.Collections.Generic;
using System.Linq;
using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class RandomTowerAttackerUnitTargetSelector : TargetSelector
    {
        private HashSet<Unit> towerAttackers = new();

        private System.Random random = new();
        
        private void Start()
        {
            var towerHealth = Tower.Instance.GetComponent<Health>();
            towerHealth.OnDamageReceived += OnTowerDamageReceived;
            DeathManager.Instance.OnUnitDeath += HandleUnitDeath;
        }

        private void OnTowerDamageReceived(Health towerAttacker)
        {
            if (towerAttacker == null)
            {
                return;
            }

            var unit = towerAttacker.GetComponent<Unit>();
            if (unit != null)
            {
                towerAttackers.Add(unit);
            }
        }
        
        private void HandleUnitDeath(Unit diedUnit)
        {
            towerAttackers.Remove(diedUnit);
        }

        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            var towerAttackerUnits = towerAttackers.ToArray();
            var potentialTargets = new List<Unit>();
            foreach (var towerAttackerUnit in towerAttackerUnits)
            {
                var distance = Vector3.Distance(towerAttackerUnit.Transform.position, selectorPosition);
                if (!towerAttackerUnit.gameObject.activeInHierarchy || distance > attackRange)
                {
                    continue;
                }

                potentialTargets.Add(towerAttackerUnit);
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