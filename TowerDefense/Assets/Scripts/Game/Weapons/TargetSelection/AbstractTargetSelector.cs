using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public abstract class AbstractTargetSelector : MonoBehaviour
    {
        public abstract TargetInfo[] SelectTargets(Vector3 selectorPosition, float selectRange);
    }
}