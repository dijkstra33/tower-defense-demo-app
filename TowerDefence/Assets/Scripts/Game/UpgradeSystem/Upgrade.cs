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

        public abstract bool IsApplicable(UpgradeType upgradeType, WeaponType weaponType);
        public abstract float GetUpgradeValue();

        public abstract string GetValueText();
    }
}