using System;
using Game.AttributeSystem.Buffs;
using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem.Upgrades
{
    public class Upgrade : ScriptableObject
    {
        public BuffHolderType BuffHolderType => buffHolderType;
        [SerializeField]
        private BuffHolderType buffHolderType;
        
        public AbstractBuff Buff => buff;
        [SerializeField]
        private AbstractBuff buff;
        
        public int Price => price;
        [SerializeField]
        private int price;

        public Sprite Sprite => sprite;
        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        private string valueText;
        public string GetValueText() => 
            string.Format(valueText, buff.GetValueForText(AttackContext.Empty));

        public bool IsUseless()
        {
            switch (BuffHolderType)
            {
                case BuffHolderType.SplashWeapon:
                case BuffHolderType.FocusFireWeapon:
                case BuffHolderType.DebuffWeapon:
                case BuffHolderType.HealthStealWeapon:
                case BuffHolderType.VengeanceWeapon:
                    var hasWeapon = false;
                    foreach (var towerWeapon in Tower.Instance.Weapons)
                    {
                        hasWeapon |= towerWeapon.GetComponent<BuffHolder>().Type == BuffHolderType;
                    }
                    return !hasWeapon;
                case BuffHolderType.Tower:
                    return false;
                case BuffHolderType.Other:
                    return true;
                default:
                    throw new Exception($"Buff holder type {buffHolderType} is not supported!");
            }
        }
    }
}