using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class TowerTargetSelector : AbstractTargetSelector
    {
        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange)
        {
            var tower = Tower.Instance;
            var distanceToTarget = Vector3.Distance(tower.gameObject.transform.position, selectorPosition);
            if (distanceToTarget > attackRange)
            {
                return null;
            }
            
            var towerHealth = tower.GetComponent<Health>();
            return new []
            {
                new TargetInfo(towerHealth, tower.gameObject.transform),
            };
        }
    }
}