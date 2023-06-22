using Game.Weapons;
using UnityEngine;

namespace Game.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Game/Upgrades/Add Value")]
    public class AddValueUpgrade : Upgrade
    {
        [SerializeField]
        private WeaponType weaponType;
        
        [SerializeField]
        private UpgradeType upgradeType;
        
        [SerializeField]
        private float value;

        public override bool IsApplicable(UpgradeType upgradeType, WeaponType weaponType)
        {
            return this.upgradeType == upgradeType && this.weaponType == weaponType;
        }

        public override float GetUpgradeValue(AttackContext _) => value;

        public override string GetValueText()
        {
            return string.Format(valueText, value);
        }
    }
}