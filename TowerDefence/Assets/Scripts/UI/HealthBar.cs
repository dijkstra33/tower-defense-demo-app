using System;
using Game.HealthSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image healthImage;
        
        [SerializeField]
        private TMP_Text healthText;
    
        [SerializeField]
        private Health health;

        private float interpolatedCurrentValue;
        private int prevCurrentHealth;
        private int prevMaxHealth;

        private void Start()
        {
            interpolatedCurrentValue = health.CurrentValue;
        }

        private void Update()
        {
            interpolatedCurrentValue = Mathf.Lerp(interpolatedCurrentValue, health.CurrentValue, Time.deltaTime);
            healthImage.fillAmount = interpolatedCurrentValue / health.MaxValue;
            if (IsHealthChanged())
            {
                healthText.text = $"{health.CurrentValue}/{health.MaxValue}";
            }
        }

        private bool IsHealthChanged()
        {
            if (prevCurrentHealth == health.CurrentValue && prevMaxHealth == health.MaxValue)
            {
                return false;
            }

            prevCurrentHealth = health.CurrentValue;
            prevMaxHealth = health.MaxValue;
            return true;
        }
    }
}
