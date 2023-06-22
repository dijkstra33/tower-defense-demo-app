using Game.Weapons;
using UnityEngine;

namespace Game.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Game/Upgrades/Add Damage Per Each Target")]
    public class AddDamagePerEachTargetUpgrade : Upgrade
    {
        [SerializeField]
        private WeaponType weaponType;

        [SerializeField]
        private float damagePerEachTarget;
        
        public override bool IsApplicable(UpgradeType upgradeType, WeaponType weaponType)
        {
            return upgradeType == UpgradeType.Damage && this.weaponType == weaponType;
        }

        public override float GetUpgradeValue(AttackContext context)
        {
            var targetsCount = context?.Targets?.Length ?? 0;
            return targetsCount * damagePerEachTarget;
        }

        public override string GetValueText()
        {
            return string.Format(valueText, damagePerEachTarget);
        }
    }
}