using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public abstract class TargetSelector : MonoBehaviour
    {
        public abstract TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange);
        
        protected bool MatchDistanceAndActive(Vector3 selectorPosition, GameObject target, float attackRange)
        {
            var distance = Vector3.Distance(target.transform.position, selectorPosition);
            return target.gameObject.activeInHierarchy && distance <= attackRange;
        }
    }
}