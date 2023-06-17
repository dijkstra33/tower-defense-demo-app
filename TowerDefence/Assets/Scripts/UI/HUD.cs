using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        public static HUD Instance { get; private set; }

        [SerializeField]
        private Button restartButton; 

        private void Awake()
        {
            restartButton.onClick.AddListener(RestartGameClicked);
            Instance = this;
        }

        public void ShowGameEndScreen()
        {
            // TODO: here will show game statistics and other stuff.
            restartButton.gameObject.SetActive(true);
        }

        private void RestartGameClicked()
        {
            GameManager.RestartLevel();
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}