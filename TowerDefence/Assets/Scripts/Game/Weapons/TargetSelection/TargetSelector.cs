using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public abstract class TargetSelector : MonoBehaviour
    {
        public abstract TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange);
    }
}