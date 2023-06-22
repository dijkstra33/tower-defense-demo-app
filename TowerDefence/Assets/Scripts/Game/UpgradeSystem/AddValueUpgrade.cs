using Game.Weapons;
using UnityEngine;

namespace Game.UpgradeSystem
{
    [CreateAssetMenu(menuName = "Game/Upgrades/Add Value Upgrade")]
    public class AddValueUpgrade : Upgrade
    {
        [SerializeField]
        private WeaponType weaponType;
        
        [SerializeField]
        private UpgradeType upgradeType;
        
        [SerializeField]
        private float value;

        [SerializeField]
        private string valueText;

        public override bool IsApplicable(UpgradeType upgradeType, WeaponType weaponType)
        {
            return this.upgradeType == upgradeType && this.weaponType == weaponType;
        }

        public override float GetUpgradeValue() => value;

        public override string GetValueText()
        {
            return string.Format(valueText, value);
        }
    }
}