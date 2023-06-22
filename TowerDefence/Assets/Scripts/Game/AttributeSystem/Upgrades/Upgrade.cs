using Game.AttributeSystem.Buffs;
using Game.Weapons;
using UnityEngine;

namespace Game.AttributeSystem.Upgrades
{
    public class Upgrade : ScriptableObject
    {
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
    }
}