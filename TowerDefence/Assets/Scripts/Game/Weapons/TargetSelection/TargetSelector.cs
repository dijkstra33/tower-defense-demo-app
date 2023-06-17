using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public abstract class TargetSelector : ScriptableObject
    {
        public abstract TargetInfo? SelectTarget(Vector3 selectorPosition, float attackRange);
    }
}