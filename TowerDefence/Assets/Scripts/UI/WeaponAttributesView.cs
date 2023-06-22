using Game;
using Game.AttributeSystem;
using Game.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponAttributesView : MonoBehaviour
    {
        [SerializeField]
        private Image backgroundImage;
        
        [SerializeField]
        private TMP_Text weaponTypeText;

        [SerializeField]
        private TMP_Text weaponDamageText;

        private int prevWeaponDamage = -1;

        [SerializeField]
        private TMP_Text weaponAttackSpeedText;
        
        private float prevWeaponAttackSpeed = -1f;
        
        [SerializeField]
        private TMP_Text weaponAttackRangeText;
        
        private float prevWeaponAttackRange = -1f;

        private AbstractWeapon weapon;

        public void SetData(AbstractWeapon weapon)
        {
            this.weapon = weapon;

            backgroundImage.color = weapon.Color;
            weaponTypeText.text = weapon.WeaponType.ToString();
        }

        private void Update()
        {
            if (weapon == null)
            {
                return;
            }

            var attackDamage = (int)weapon.AttributeOwner.GetValue(AttributeType.Damage);
            if (prevWeaponDamage != attackDamage)
            {
                prevWeaponDamage = attackDamage;
                weaponDamageText.text = attackDamage.ToString();
            }

            var attackSpeed = weapon.GetAttackSpeed();
            if (!Mathf.Approximately(prevWeaponAttackSpeed, attackSpeed))
            {
                prevWeaponAttackSpeed = attackSpeed;
                weaponAttackSpeedText.text = $"{attackSpeed:F1}";
            }

            var attackRange = weapon.AttributeOwner.GetValue(AttributeType.AttackRange);
            if (!Mathf.Approximately(prevWeaponAttackRange, attackRange))
            {
                prevWeaponAttackRange = attackRange;
                weaponAttackRangeText.text = $"{attackRange:F1}";
            }
        }
    }
}