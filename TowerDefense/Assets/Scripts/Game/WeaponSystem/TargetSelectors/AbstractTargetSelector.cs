using UnityEngine;

namespace Game.WeaponSystem.TargetSelectors
{
    public abstract class AbstractTargetSelector : MonoBehaviour
    {
        public abstract TargetInfo[] SelectTargets(Vector3 selectorPosition, float selectRange);
    }
}