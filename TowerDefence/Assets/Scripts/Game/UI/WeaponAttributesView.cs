using Game.AttributeSystem;
using Game.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class WeaponAttributesView : MonoBehaviour
    {
        [SerializeField]
        private Image weaponNameBackground;

        [SerializeField]
        private TMP_Text weaponNameText;

        [SerializeField]
        private Image weaponIcon;

        [SerializeField]
        private TMP_Text weaponDamageText;

        private int prevWeaponDamage = -1;

        [SerializeField]
        private TMP_Text weaponAttackSpeedText;
        
        private float prevWeaponAttackSpeed = -1f;
        
        [SerializeField]
        private TMP_Text weaponAttackRangeText;
        
        private float prevWeaponAttackRange = -1f;

        private AbstractAttributeOwner weaponAttributeOwner;

        public void SetData(WeaponVisualData weaponVisualData, AbstractAttributeOwner weaponAttributeOwner)
        {
            weaponNameBackground.color = weaponVisualData.Color;
            weaponNameText.text = weaponVisualData.Name;
            weaponIcon.sprite = weaponVisualData.Icon;

            this.weaponAttributeOwner = weaponAttributeOwner;
        }

        private void Update()
        {
            if (weaponAttributeOwner == null)
            {
                return;
            }

            var attackDamage = (int)weaponAttributeOwner.GetValue(AttributeType.Damage);
            if (prevWeaponDamage != attackDamage)
            {
                prevWeaponDamage = attackDamage;
                weaponDamageText.text = $"Damage:\t{attackDamage}";
            }

            var attackInterval = weaponAttributeOwner.GetValue(AttributeType.AttackInterval);
            var attackSpeed = 1 / attackInterval;
            if (!Mathf.Approximately(prevWeaponAttackSpeed, attackSpeed))
            {
                prevWeaponAttackSpeed = attackSpeed;
                weaponAttackSpeedText.text = $"Speed:\t{attackSpeed:F1}";
            }

            var attackRange = weaponAttributeOwner.GetValue(AttributeType.AttackRange);
            if (!Mathf.Approximately(prevWeaponAttackRange, attackRange))
            {
                prevWeaponAttackRange = attackRange;
                weaponAttackRangeText.text = $"Range:\t{attackRange:F1}";
            }
        }
    }
}