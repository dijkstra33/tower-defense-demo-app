using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public abstract class AbstractTemplatedTargetSelector<TTarget> : AbstractTargetSelector
    {
        public override TargetInfo[] SelectTargets(Vector3 selectorPosition, float selectRange)
        {
            var potentialTargets = GetPotentialTargets();
            var filteredTargets = new List<TTarget>();
            foreach (var potentialTarget in potentialTargets)
            {
                if (MatchObligatoryFilter(potentialTarget, selectorPosition, selectRange) 
                    && MatchFilter(potentialTarget))
                {
                    filteredTargets.Add(potentialTarget);
                }
            }

            return FinalizeResult(filteredTargets, selectorPosition, selectRange);
        }

        protected abstract TTarget[] GetPotentialTargets();
        protected abstract bool MatchObligatoryFilter(TTarget potentialTarget, Vector3 selectorPosition, float selectRange);
        protected virtual bool MatchFilter(TTarget potentialTarget) => true;

        protected abstract TargetInfo[] FinalizeResult(List<TTarget> filteredTargets, Vector3 selectorPosition, float selectRange);
    }
}