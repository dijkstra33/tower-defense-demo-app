using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public abstract class TemplatedTargetSelector<TTarget> : AbstractTargetSelector
    {
        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float selectRange)
        {
            var potentialTargets = GetPotentialTargets();
            var filteredTargets = new List<TTarget>();
            foreach (var potentialTarget in potentialTargets)
            {
                if (MatchFilter(potentialTarget, selectorPosition, selectRange))
                {
                    filteredTargets.Add(potentialTarget);
                }
            }

            return FinalizeResult(filteredTargets, selectorPosition, selectRange);
        }

        protected abstract TTarget[] GetPotentialTargets();
        protected abstract bool MatchFilter(TTarget potentialTarget, Vector3 selectorPosition, float selectRange);
        protected abstract TargetInfo[] FinalizeResult(List<TTarget> filteredTargets, Vector3 selectorPosition, float selectRange);
    }
}