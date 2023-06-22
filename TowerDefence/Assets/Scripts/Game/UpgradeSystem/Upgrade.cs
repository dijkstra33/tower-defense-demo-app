using Game.Weapons;
using UnityEngine;

namespace Game.UpgradeSystem
{
    public abstract class Upgrade : ScriptableObject
    {
        public int Price => price;
        [SerializeField]
        private int price;

        public Sprite Sprite => sprite;
        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        protected string valueText;

        public abstract bool IsApplicable(UpgradeType upgradeType, WeaponType weaponType);
        public abstract float GetUpgradeValue(AttackContext context);

        public abstract string GetValueText();
    }
}