using Game.HealthSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class WorldSpaceHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image backgroundImage;
        
        [SerializeField]
        private Image healthImage;

        private Health owherHealth;
        private Transform cachedTransform;
        
        private Vector3 positionOffset;

        private void Awake()
        {
            cachedTransform = transform;
            
            owherHealth = GetComponentInParent<Health>();
            owherHealth.OnValueChanged += HandleHealthValueChanged;

            positionOffset = cachedTransform.position - owherHealth.transform.position;
            
            var worldSpaceCanvas = GameObject.FindWithTag("WorldSpaceCanvas");
            gameObject.transform.SetParent(worldSpaceCanvas.transform);
            
            cachedTransform.rotation = Camera.main.transform.rotation;
        }

        private void Update()
        {
            if (!owherHealth.gameObject.activeSelf)
            {
                backgroundImage.gameObject.SetActive(false);
                healthImage.gameObject.SetActive(false);
                return;
            }

            backgroundImage.gameObject.SetActive(true);
            healthImage.gameObject.SetActive(true);

            cachedTransform.position = owherHealth.gameObject.transform.position + positionOffset;
        }

        private void HandleHealthValueChanged()
        {
            healthImage.fillAmount = 1f * owherHealth.CurrentValue / owherHealth.MaxValue;
        }
    }
}