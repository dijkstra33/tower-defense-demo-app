using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public abstract class TargetSelector : ScriptableObject
    {
        public abstract TargetInfo[] SelectTargets(Vector3 selectorPosition, float attackRange);
    }
}