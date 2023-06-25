using Game.HealthSystem;
using Game.WeaponSystem;
using UnityEngine;

namespace Game.AttributeSystem.Buffs
{
    public class AddDamagePerEachTowerAttackByTargetBuff : AbstractBuff
    {
        [SerializeField]
        private float damagePerEachTowerAttackByTarget;
        
        public override AttributeType BuffedAttributeType => AttributeType.Damage;

        public override float GetValue(AttackContext context)
        {
            var targetUnit =
                context.Target.Health != null
                    ? context.Target.Health.GetComponent<Unit>()
                    : null;

            if (targetUnit == null)
            {
                return 0;
            }

            var towerBattleContext = Tower.Instance.GetComponent<BattleContext>();
            var towerHitsCount = 0;
            foreach (var targetUnitWeapon in targetUnit.Weapons)
            {
                towerHitsCount += towerBattleContext.GetHitsCountBy(targetUnitWeapon);
            }

            return towerHitsCount * damagePerEachTowerAttackByTarget;
        }

        public override float GetValueForText(AttackContext context) => damagePerEachTowerAttackByTarget;
    }
}