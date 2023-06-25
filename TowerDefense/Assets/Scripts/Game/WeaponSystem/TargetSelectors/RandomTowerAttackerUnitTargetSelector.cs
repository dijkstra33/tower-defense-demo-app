using Game.HealthSystem;

namespace Game.WeaponSystem.TargetSelectors
{
    public class RandomTowerAttackerUnitTargetSelector : RandomUnitTargetSelector
    {
        private BattleContext towerBattleContext;
        
        private void Start()
        {
            towerBattleContext = Tower.Instance.GetComponent<BattleContext>();
        }

        protected override bool MatchFilter(Unit potentialTarget)
        {
            foreach (var unitWeapon in potentialTarget.Weapons)
            {
                if (towerBattleContext.GetHitsCountBy(unitWeapon) > 0)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}