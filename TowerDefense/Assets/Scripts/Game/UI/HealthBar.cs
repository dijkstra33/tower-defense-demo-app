using Game.HealthSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image healthImage;
        
        [SerializeField]
        private TMP_Text healthText;
    
        [SerializeField]
        private Health health;

        [SerializeField]
        private float healthUpdateSpeedSeconds;

        private float currentFillAmount;
        private float targetFillAmount;
        private float elapsedTime;

        private void Start()
        {
            health.OnValueChanged += HandleHealthValueChanged;
            
            currentFillAmount = 1f * health.CurrentValue / health.MaxValue;
            HandleHealthValueChanged();
        }

        private void HandleHealthValueChanged()
        {
            elapsedTime = 0f;
            targetFillAmount = 1f * health.CurrentValue / health.MaxValue;
            healthText.text = $"{health.CurrentValue}/{health.MaxValue}";
        }

        private void Update()
        {
            if (elapsedTime > healthUpdateSpeedSeconds)
            {
                return;
            }

            elapsedTime += Time.deltaTime;
            if (elapsedTime < healthUpdateSpeedSeconds)
            {
                currentFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, elapsedTime / healthUpdateSpeedSeconds);
                healthImage.fillAmount = currentFillAmount;
            }
            else
            {
                currentFillAmount = targetFillAmount;
                healthImage.fillAmount = currentFillAmount;
            }
        }
    }
}
    