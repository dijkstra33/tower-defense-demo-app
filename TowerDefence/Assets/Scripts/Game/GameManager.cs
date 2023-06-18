using Core;
using Game.HealthSystem;
using UI;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : SingletonMoneBehaviour<GameManager>
    {
        public bool GameOver => gameOver;
        private bool gameOver = false;
        
        protected override void Awake()
        {
            base.Awake();
            DeathManager.Instance.OnTowerDeath += HandleTowerDeath;
        }

        private void HandleTowerDeath(Tower tower)
        {
            gameOver = true;
            HUD.Instance.ShowGameOverScreen();
        }

        public static void RestartLevel()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}