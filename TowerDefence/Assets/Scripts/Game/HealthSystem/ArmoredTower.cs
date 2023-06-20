using Game.UpgradeSystem;
using UnityEngine;

namespace Game.HealthSystem
{
    [RequireComponent(typeof(Tower), typeof(Health))]
    public class ArmoredTower : Armored
    {
        [SerializeField]
        private int baseArmor;
        
        public override int GetArmor() => baseArmor + (int)UpgradeManager.Instance.GetUpgradeValue(UpgradeType.Armor, null);
    }
}