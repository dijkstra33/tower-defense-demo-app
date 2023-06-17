using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    [CreateAssetMenu(menuName = "Game/TargetSelectors/TowerTargetSelector")]
    public class TowerTargetSelector : TargetSelector
    {
        public override TargetInfo? SelectTarget(Vector3 selectorPosition)
        {
            var tower = Tower.Instance;
            var distanceToTarget = Vector3.Distance(tower.gameObject.transform.position, selectorPosition);
            if (distanceToTarget > attackRange)
            {
                return null;
            }
            
            var towerHealth = tower.GetComponent<Health.Health>();
            return new TargetInfo(towerHealth, tower.gameObject.transform);
        }
    }
}