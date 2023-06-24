using Game.AttributeSystem.Buffs;
using Game.HealthSystem;
using UnityEngine;

namespace Game.Weapons.TargetSelection
{
    public class RandomTowerAttackerUnitTargetSelector : RandomUnitTargetSelector
    {
        private BattleContext towerBattleContext;
        
        private void Start()
        {
            towerBattleContext = Tower.Instance.GetComponent<BattleContext>();
        }

        protected override bool MatchFilter(Unit potentialTarget, Vector3 selectorPosition, float selectRange)
        {
            if (!base.MatchFilter(potentialTarget, selectorPosition, selectRange))
            {
                return false;
            }
            
            foreach (var unitWeapon in potentialTarget.Weapons)
            {
                var unitWeaponBuffHolder = unitWeapon.GetComponent<BuffHolder>();
                if (towerBattleContext.GetHitsCountBy(unitWeaponBuffHolder) > 0)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}